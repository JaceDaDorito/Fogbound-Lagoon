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
    public static class IAddressableKeyProviderExt
    {
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
    public interface IAddressableKeyProvider<T>
    {
        string Key { get; }
        T Addressable { set; }
        
    }
}
