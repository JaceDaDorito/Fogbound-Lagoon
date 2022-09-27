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
using FBLStage.Content;

namespace FBLStage.Utils
{
    public class SlipDccsHandler
    {
        public void Init()
        {
            SlipDccs[] allSlipDccs = FBLContent.SlipDccsArray;
            foreach (SlipDccs slipDccs in allSlipDccs)
            {
                slipDccs.ResolveAddressableCategories();
            }
        }

    }
}
