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
    public struct AddressableCategory
    {
        public DirectorCardCategorySelection.Category ToCategory()
        {
            if (!hasResolved)
            {
                foreach (IAddressableKeyProvider<SpawnCard> provider in addressableCards)
                {
                    provider.Resolve();
                }
                hasResolved = true;
            }
            return new DirectorCardCategorySelection.Category
            {
                name = name,
                cards = addressableCards,
                selectionWeight = selectionWeight
            };
        }
        [Tooltip("A name to help identify this addressable category")]
        public string name;
        //[SerializeField]
        public AddressableDirectorCard[] addressableCards;

        public float selectionWeight;

        private bool hasResolved;
    }
}
