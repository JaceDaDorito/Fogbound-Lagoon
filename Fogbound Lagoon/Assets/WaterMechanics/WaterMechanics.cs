using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using BepInEx;
using RoR2.ContentManagement;
using UnityEngine;
using RoR2;
using System.Linq;
using System.Security;
using BepInEx.Configuration;
using BepInEx.Bootstrap;
using System.Runtime.CompilerServices;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[assembly: HG.Reflection.SearchableAttribute.OptIn]

namespace WaterMechanics
{
    [BepInPlugin(GUID, Name, Version)]
    public class WaterMechanics : BaseUnityPlugin
    {
        public const string Author = "JaceDaDorito";

        public const string Name = nameof(WaterMechanics);

        public const string Version = "1.0.0";

        public const string GUID = Author + "." + Name;

        public static WaterMechanics instance;

        private void Awake()
        {
            instance = this;

            Log.Init(Logger);
        }
    }
}
