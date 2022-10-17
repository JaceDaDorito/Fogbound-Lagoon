using System.Security.Permissions;
using BepInEx;
using FBLStage.Content;
using R2API;
using RoR2.ContentManagement;
using FBLStage.Utils;
using System.Collections.Generic;
using UnityEngine;
using RoR2;

//Copied from Nuketown

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete

namespace FBLStage
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [R2API.Utils.R2APISubmoduleDependency(nameof(PrefabAPI), nameof(SoundAPI))]
    [BepInPlugin(GUID, Name, Version)]
    public class FBLStage : BaseUnityPlugin
    {
        public const string Author = "JaceDaDorito";

        public const string Name = nameof(FBLStage);

        public const string Version = "1.0.0";

        public const string GUID = Author + "." + Name;

        public static FBLStage instance;

        private void Awake()
        {
            instance = this;

            Log.Init(Logger);

            ContentManager.collectContentPackProviders += GiveToRoR2OurContentPackProviders;

            //new SlipDccsHandler().Init();
        }

        public void LoadMusicBank()
        {
            var d = new SoundAPI.Music.CustomMusicData();
            d.BanksFolderPath = Paths.PluginPath;
            Log.Debug(d.BanksFolderPath);
            d.BepInPlugin = Info.Metadata;
            d.InitBankName = "fbl_Init";
            d.PlayMusicSystemEventName = "Play_TropicOfCapricorn";
            d.SoundBankName = "fblmusicsoundbank.bnk";

            /*
            var mainSceneTrackDef = ScriptableObject.CreateInstance<SoundAPI.Music.CustomMusicTrackDef>();
            mainSceneTrackDef.cachedName = "FBLToC";
            mainSceneTrackDef.SoundBankName = d.SoundBankName;
            mainSceneTrackDef.CustomStates = new List<SoundAPI.Music.CustomMusicTrackDef.CustomState>();

            var cstate1 = new SoundAPI.Music.CustomMusicTrackDef.CustomState();
            cstate1.GroupId = 3148870334U;
            cstate1.StateId = 145640315U;
            mainSceneTrackDef.CustomStates.Add(cstate1);

            var cstate2 = new SoundAPI.Music.CustomMusicTrackDef.CustomState();
            cstate2.GroupId = 792781730U;
            cstate2.StateId = 89505537U;
            mainSceneTrackDef.CustomStates.Add(cstate2);

            Log.Debug(FBLContent.FBLSceneDef == null);
            FBLContent.FBLSceneDef.mainTrack = mainSceneTrackDef;
            Log.Debug(FBLContent.FBLSceneDef.mainTrack);
            SoundAPI.Music.Add(d);*/
            
        }

        private static void GiveToRoR2OurContentPackProviders(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(new ContentProvider());
        }
    }
}
