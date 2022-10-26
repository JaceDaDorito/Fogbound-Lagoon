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
using System.Linq;
using FBLStage.Content;

namespace FBLStage.Utils
{
    public static class SlipDirectorUtils
    {
        [SystemInitializer(new Type[] { })]
        public static void Init()
        {
            SlipDccs[] allSlipDccs = FBLContent.SlipDccsArray;
            foreach (SlipDccs slipDccs in allSlipDccs)
            {
                slipDccs.ResolveAddressableCategories();
            }
            SlipFamilyDccs[] allSlipFamilyDccs = FBLContent.SlipFamilyArray;
            foreach (SlipFamilyDccs slipFamilyDccs in allSlipFamilyDccs)
            {
                slipFamilyDccs.ResolveAddressableCategories();
            }
            SlipDccsPool[] allSlipDccsPools = FBLContent.SlipDccsPoolsArray;
            foreach (SlipDccsPool slipDccsPool in allSlipDccsPools)
            {
                slipDccsPool.ResolveAddressableCategories();
            }
        }
        public static void Resolve<T>(this IAddressableKeyProvider<T> provider)
        {
            if (!string.IsNullOrEmpty(provider.Key))
            {
                T addressable = Addressables.LoadAssetAsync<T>(provider.Key).WaitForCompletion();
                if (addressable == null)
                {
                    Log.Warning(provider + ": Addressable key [" + provider.Key + "] was provided, but returned null!");
                }
                provider.Addressable = addressable;
            }
        }
    }
}