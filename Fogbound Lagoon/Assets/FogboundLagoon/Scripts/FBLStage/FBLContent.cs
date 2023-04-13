﻿using System;
using System.Collections;
using System.Linq;
using RoR2;
using RoR2.ContentManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using FBLStage.Utils;
using RoR2.ExpansionManagement;
using System.Collections.Generic;
using RoR2.Networking;

namespace FBLStage.Content
{
    public static class FBLContent
    {
        internal const string SoundBankFileName = "fblsoundbank.bnk";
        //internal const string MusicBankFileName = "fblmusicsoundbank.bnk";

        internal const string ScenesAssetBundleFileName = "fblstage";
        internal const string AssetsAssetBundleFileName = "fblassets";

        private static uint _soundbankId;
        internal static NetworkSoundEventDef BadToTheBone;

        private static AssetBundle _scenesAssetBundle;
        private static AssetBundle _assetsAssetBundle;

        //internal static GameObject[] NetworkedObjectPrefabs;
        internal static UnlockableDef[] UnlockableDefs;
        internal static SlipDccs[] SlipDccsArray;
        internal static SlipFamilyDccs[] SlipFamilyArray;
        internal static SlipDccsPool[] SlipDccsPoolsArray;
        internal static SceneDef[] SceneDefs;

        internal static SceneDef FBLSceneDef;
        internal static MusicTrackDef FBLMusicDef;
        //internal static DccsPool FBLInteractablesPool;
        //internal static DccsPool FBLMonstersPool;
        internal static Sprite FBLSceneDefPreviewSprite;
        internal static Material FBLBazaarSeer;

        public static List<Material> SwappedMaterials = new List<Material>(); //debug

        public static Dictionary<string, string> ShaderLookup = new Dictionary<string, string>()
        {
            {"stubbedror2/base/shaders/hgstandard", "RoR2/Base/Shaders/HGStandard.shader"},
            {"stubbedror2/base/shaders/hgsnowtopped", "RoR2/Base/Shaders/HGSnowTopped.shader"},
            {"stubbedror2/base/shaders/hgtriplanarterrainblend", "RoR2/Base/Shaders/HGTriplanarTerrainBlend.shader"},
            {"stubbedror2/base/shaders/hgintersectioncloudremap", "RoR2/Base/Shaders/HGIntersectionCloudRemap.shader" },
            {"stubbedror2/base/shaders/hgcloudremap", "RoR2/Base/Shaders/HGCloudRemap.shader" },
            {"stubbedror2/base/shaders/hgdistortion", "RoR2/Base/Shaders/HGDistortion.shader" },
            {"stubbedcalm water/calmwater - dx11 - doublesided", "Calm Water/CalmWater - DX11 - DoubleSided.shader" },
            {"stubbedcalm water/calmwater - dx11", "Calm Water/CalmWater - DX11.shader" },
            {"stubbednature/speedtree", "RoR2/Base/Shaders/SpeedTreeCustom.shader"}
        };

        internal static void LoadSoundBank(string assetsFolderFullPath)
        {
            var akResult = AkSoundEngine.AddBasePath(assetsFolderFullPath);
            if (akResult == AKRESULT.AK_Success)
            {
                Log.Info($"Added bank base path : {assetsFolderFullPath}");
            }
            else
            {
                Log.Error(
                    $"Error adding base path : {assetsFolderFullPath} " +
                    $"Error code : {akResult}");
            }

            akResult = AkSoundEngine.LoadBank(SoundBankFileName, out _soundbankId);
            if (akResult == AKRESULT.AK_Success)
            {
                Log.Info($"Added bank : {SoundBankFileName}");
            }
            else
            {
                Log.Error(
                    $"Error loading bank : {SoundBankFileName} " +
                    $"Error code : {akResult}");
            }

        }

        internal static IEnumerator LoadAssetBundlesAsync(AssetBundle scenesAssetBundle, AssetBundle assetsAssetBundle, IProgress<float> progress, ContentPack contentPack)
        {
            _scenesAssetBundle = scenesAssetBundle;
            _assetsAssetBundle = assetsAssetBundle;

            yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<Material[]>)((assets) =>
            {
                var materials = assets;


                foreach (Material material in materials)
                {
                    Log.Debug(material.name + " " + material.shader);
                    if (!material.shader.name.StartsWith("Stubbed")) { continue; }

                    var replacementShader = Addressables.LoadAssetAsync<Shader>(ShaderLookup[material.shader.name.ToLower()]).WaitForCompletion();
                    Log.Debug(replacementShader.name);
                    if (replacementShader)
                    {
                        material.shader = replacementShader;
                        SwappedMaterials.Add(material);
                    }
                    Log.Debug(material.name + " " + material.shader);
                }
            }));

            /*yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<GameObject[]>)((assets) =>
            {
                var networkedAssets = assets.Where(a => a.GetComponent<NetworkIdentity>()).ToArray();
                NetworkedObjectPrefabs = networkedAssets;
                foreach (var prefab in NetworkedObjectPrefabs)
                {
                    prefab.RegisterNetworkPrefab();
                }
            }));*/

            yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<NetworkSoundEventDef[]>)((assets) =>
            {
                BadToTheBone = assets.First(a => a.name == "FBL_BadtotheBone");
                contentPack.networkSoundEventDefs.Add(assets);
            }));

            yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<UnlockableDef[]>)((assets) =>
            {
                UnlockableDefs = assets;
                contentPack.unlockableDefs.Add(assets);
            }));

            //I didn't showcase stuff relating to slipDccs yet

            yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<SlipDccs[]>)((assets) =>
            {
                SlipDccsArray = assets;
            }));

            yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<SlipFamilyDccs[]>)((assets) =>
            {
                SlipFamilyArray = assets;
            }));

            yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<SlipDccsPool[]>)((assets) =>
            {
                SlipDccsPoolsArray = assets;
                //FBLInteractablesPool = DccsPools.First(dp => dp.name == "dpFogboundLagoonInteractables");
                //FBLMonstersPool = DccsPools.First(dp => dp.name == "dpFogboundLagoonMonsters");
            }));

            yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<Sprite[]>)((assets) =>
            {
                FBLSceneDefPreviewSprite = assets.First(a => a.name == "texFBLScenePreview");
            }));

            yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<SceneDef[]>)((assets) =>
            {
                SceneDefs = assets;
                FBLSceneDef = SceneDefs.First(sd => sd.baseSceneNameOverride == "FBLScene" );
                Log.Debug(FBLSceneDef.nameToken);
                contentPack.sceneDefs.Add(assets);
            }));

            //FBLStage.Utils.SlipDirectorUtils().Init();

            var matBazaarSeerWispgraveyardRequest = Addressables.LoadAssetAsync<Material>("RoR2/Base/bazaar/matBazaarSeerWispgraveyard.mat");
            while (!matBazaarSeerWispgraveyardRequest.IsDone)
            {
                yield return null;
            }
            FBLBazaarSeer = UnityEngine.Object.Instantiate(matBazaarSeerWispgraveyardRequest.Result);
            FBLBazaarSeer.mainTexture = FBLSceneDefPreviewSprite.texture;
            FBLSceneDef.portalMaterial = FBLBazaarSeer;

            //FBLStage.instance.LoadMusicBank();

            //Commented code below shows you how to reuse vanilla music for normal gameplay
            //RoR2/Base/Common/muFULLSong07.asset
            var mainTrackDefRequest = Addressables.LoadAssetAsync<MusicTrackDef>("RoR2/Base/Common/muFULLSong06.asset");
            while (!mainTrackDefRequest.IsDone)
            {
                yield return null;
            }
            var bossTrackDefRequest = Addressables.LoadAssetAsync<MusicTrackDef>("RoR2/Base/Common/muSong16.asset");
            while (!bossTrackDefRequest.IsDone)
            {
                yield return null;
            }
            FBLSceneDef.mainTrack = mainTrackDefRequest.Result;
            FBLSceneDef.bossTrack = bossTrackDefRequest.Result;

            var stage3SceneCollectionRequest = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage3.asset");
            while (!stage3SceneCollectionRequest.IsDone)
            {
                yield return null;
            }

            AddFBLSceneDefToStage3SceneCollection(stage3SceneCollectionRequest);

            var stage4SceneCollectionRequest = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage4.asset");
            while (!stage4SceneCollectionRequest.IsDone)
            {
                yield return null;
            }
            FBLSceneDef.destinationsGroup = stage4SceneCollectionRequest.Result;

            /*Make sure the expansion is being filled in appropriate fields.
             *This code here assumes that you want to fill the SoTV expansion on the first element of the "Included If Conditions Met" array in your first PoolCategory.
             *
             *If you don't want expansion support or if your stage already relies on the expansion, remove this code below. Don't remove "yield break;" though.
             */

            /*ExpansionDef[] dlcExpansion = { Addressables.LoadAssetAsync<ExpansionDef>("RoR2/DLC1/Common/DLC1.asset").WaitForCompletion() };
            DccsPool.Category intStandard = FBLInteractablesPool.poolCategories[0];
            if(intStandard.includedIfConditionsMet[0] != null)
                intStandard.includedIfConditionsMet[0].requiredExpansions = dlcExpansion;

            DccsPool.Category monStandard = FBLMonstersPool.poolCategories[0];
            if (monStandard.includedIfConditionsMet[0] != null)
                monStandard.includedIfConditionsMet[0].requiredExpansions = dlcExpansion;

            yield break;*/
        }

        private static void AddFBLSceneDefToStage3SceneCollection(AsyncOperationHandle<SceneCollection> stage3SceneCollectionRequest)
        {
            var sceneEntries = stage3SceneCollectionRequest.Result._sceneEntries.ToList();
            //This Loop was to make Fogbound more common, I accidently left this here.
            /*for (int i = 0; i < sceneEntries.Count; i++)
            {
                var sceneEntry = sceneEntries[i];
                sceneEntry.weightMinusOne = -0.75f;
                sceneEntries[i] = sceneEntry;
            }*/
            sceneEntries.Add(new SceneCollection.SceneEntry { sceneDef = FBLSceneDef, weightMinusOne = 0 });
            
            stage3SceneCollectionRequest.Result._sceneEntries = sceneEntries.ToArray();
        }

        internal static void Unload()
        {
            _assetsAssetBundle.Unload(true);
            _scenesAssetBundle.Unload(true);

            var akResult = AkSoundEngine.UnloadBank(_soundbankId, IntPtr.Zero);
            if (akResult == AKRESULT.AK_Success)
            {
                Log.Info($"Unloaded bank : {SoundBankFileName}.");
            }
            else
            {
                Log.Error(
                    $"Error unloading bank : {SoundBankFileName}. " +
                    $"Error code : {akResult}");
            }
        }

        private static IEnumerator LoadAllAssetsAsync<T>(AssetBundle assetBundle, IProgress<float> progress, Action<T[]> onAssetsLoaded) where T : UnityEngine.Object
        {
            var sceneDefsRequest = assetBundle.LoadAllAssetsAsync<T>();
            while (!sceneDefsRequest.isDone)
            {
                progress.Report(sceneDefsRequest.progress);
                yield return null;
            }

            onAssetsLoaded(sceneDefsRequest.allAssets.Cast<T>().ToArray());

            yield break;
        }
    }
}
