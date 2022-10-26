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

    [CreateAssetMenu(menuName = "Slipstream/SlipFamilyDccs")]
    public class SlipFamilyDccs : FamilyDirectorCardCategorySelection
    {
        public void ResolveAddressableCategories()
        {
            categories = new Category[addressableCategories.Length];
            for (int i = 0; i < categories.Length; i++)
            {
                categories[i] = addressableCategories[i].ToCategory();
            }
            //literally no idea why this makes things work, but if you remove it everything breaks :) - groove
            On.RoR2.DirectorCardCategorySelection.CopyFrom += DirectorCardCategorySelection_CopyFrom;
        }

        private void DirectorCardCategorySelection_CopyFrom(On.RoR2.DirectorCardCategorySelection.orig_CopyFrom orig, DirectorCardCategorySelection self, DirectorCardCategorySelection src)
        {
            orig(self, src);
        }

        [SerializeField]
        [Tooltip("Categories populated by addressable keys or spawn cards. Addressable keys are added as spawn cards at runtime.")]
        public AddressableCategory[] addressableCategories = Array.Empty<AddressableCategory>();
    }

}