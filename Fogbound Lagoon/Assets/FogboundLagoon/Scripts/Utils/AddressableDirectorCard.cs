using UnityEngine.AddressableAssets;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;
using R2API;
using RoR2.Items;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace FBLStage.Utils
{
    [Serializable]
    public class AddressableDirectorCard : DirectorCard
    {
        [Tooltip("An optional addressable key to load a vanilla spawn card")]
        public string spawnCardKey;
        
    }

}
