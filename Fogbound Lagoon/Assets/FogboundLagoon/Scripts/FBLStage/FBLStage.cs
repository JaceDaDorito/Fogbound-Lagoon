using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using BepInEx;
using FBLStage.Content;
using RoR2.ContentManagement;
using FBLStage.Utils;
using UnityEngine;
using RoR2;
using System.Linq;
using System.Security;
using BepInEx.Configuration;
using Moonstorm;
using BepInEx.Bootstrap;
using System.Runtime.CompilerServices;
//Copied from Nuketown

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[assembly: HG.Reflection.SearchableAttribute.OptIn]

namespace FBLStage
{
    [BepInDependency("com.TeamMoonstorm.MoonstormSharedUtils", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(GUID, Name, Version)]
    public class FBLStage : BaseUnityPlugin
    {
        public const string Author = "JaceDaDorito";

        public const string Name = nameof(FBLStage);

        public const string Version = "1.0.3";

        public const string GUID = Author + "." + Name;

        public static FBLStage instance;

        private void Awake()
        {
            instance = this;

            Log.Init(Logger);

            ContentManager.collectContentPackProviders += GiveToRoR2OurContentPackProviders;

            //Just to make sure theres no NREs that slip through. Will remove once I have proper storm support.
            if (Chainloader.PluginInfos.ContainsKey("com.TeamMoonstorm.MoonstormSharedUtils"))
                AddSceneBlacklist();

            Language.collectLanguageRootFolders += CollectLanguageRootFolders;
        }

        private void Destroy()
        {
            Language.collectLanguageRootFolders -= CollectLanguageRootFolders;
        }

        /*public void LoadMusicBank()
        {
            var d = new SoundAPI.Music.CustomMusicData();
            d.BanksFolderPath = Paths.PluginPath;
            Log.Debug(d.BanksFolderPath);
            d.BepInPlugin = Info.Metadata;
            d.InitBankName = "fbl_Init";
            d.PlayMusicSystemEventName = "Play_TropicOfCapricorn";
            d.SoundBankName = "fblmusicsoundbank.bnk";

            
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
            SoundAPI.Music.Add(d);
            
        }*/

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private void AddSceneBlacklist()
        {
            Moonstorm.Components.SetupWeatherController.blacklistedScenes.Add("FBLScene");
        }

        private static void GiveToRoR2OurContentPackProviders(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(new ContentProvider());
        }

        public void CollectLanguageRootFolders(List<string> folders)
        {
            folders.Add(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(base.Info.Location), "Language"));
        }
    }
}
