using System;
using System.Collections;
using System.Linq;
using RoR2;
using RoR2.ContentManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using RoR2.ExpansionManagement;
using System.Collections.Generic;
using RoR2.Networking;
using ShaderSwapper;
using R2API;

namespace FBLStage.Content
{
    public static class FBLContent
    {
        internal const string SoundBankFileName = "fblsoundbank.bnk";

        internal const string ScenesAssetBundleFileName = "fblstage";
        internal const string AssetsAssetBundleFileName = "fblassets";

        private static uint _soundbankId;
        internal static NetworkSoundEventDef BadToTheBone;

        private static AssetBundle _scenesAssetBundle;
        private static AssetBundle _assetsAssetBundle;

        internal static UnlockableDef[] UnlockableDefs;
        private static string baseDPMonsterString = "dpFBLMonsters";
        internal static SceneDef[] SceneDefs;

        internal static SceneDef FBLSceneDef;
        internal static Sprite FBLSceneDefPreviewSprite;
        internal static Material FBLBazaarSeer;

        public static List<Material> SwappedMaterials = new List<Material>(); 

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

            var upgradeStubbedShaders = _assetsAssetBundle.UpgradeStubbedShadersAsync();
            while (upgradeStubbedShaders.MoveNext())
            {
                yield return upgradeStubbedShaders.Current;
            }

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

            /*yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<SlipDccs[]>)((assets) =>
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
                for (int i = 0; i < SlipDccsPoolsArray.Length; i++)
                {
                    if (baseDPMonsterString == SlipDccsPoolsArray[i].name) monsterDP = SlipDccsPoolsArray[i];
                }
            }));*/

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

            FBLSceneDef.portalMaterial = R2API.StageRegistration.MakeBazaarSeerMaterial((Texture2D)FBLSceneDef.previewTexture);

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

            R2API.StageRegistration.RegisterSceneDefToNormalProgression(FBLSceneDef);
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
