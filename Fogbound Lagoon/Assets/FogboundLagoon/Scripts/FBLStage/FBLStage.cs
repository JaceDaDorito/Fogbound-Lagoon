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

        public const string Version = "1.1.0";

        public const string GUID = Author + "." + Name;

        public static FBLStage instance;

        public static ConfigEntry<bool> dlcPool;
        public static ConfigEntry<bool> legacyPool;

        private void Awake()
        {
            instance = this;

            Log.Init(Logger);

            ConfigSetup();

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

        

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private void AddSceneBlacklist()
        {
            //This is Temporary
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

        private void ConfigSetup()
        {
            dlcPool =
                base.Config.Bind<bool>("Monster Pool Settings",
                                       "SoTV DLC Pool",
                                       true,
                                       "If you want to play with the non-DLC monster pool without turning off the DLC, set this to false. This doesn't effect void seeds or the void enemies that spawn out of them.");

            legacyPool =
                base.Config.Bind<bool>("Monster Pool Settings",
                                       "Legacy Pool",
                                       false,
                                       "If you want to keep the monster pool prior to version 1.1.0, set this to true. If the DLC pool is also set to false you can play the legacy non-DLC pool.");
        }
    }
}
