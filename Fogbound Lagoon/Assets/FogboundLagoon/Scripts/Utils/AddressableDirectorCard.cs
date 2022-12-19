using UnityEngine.AddressableAssets;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;
using RoR2.Items;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace FBLStage.Utils
{
    [Serializable]
    public class AddressableDirectorCard : DirectorCard, IAddressableKeyProvider<SpawnCard>
    {

        [Tooltip("An optional addressable key to load a vanilla spawn card")]
        public string spawnCardKey;

        public string Key => spawnCardKey;

        public SpawnCard Addressable { set => spawnCard = value; }
    }

}
