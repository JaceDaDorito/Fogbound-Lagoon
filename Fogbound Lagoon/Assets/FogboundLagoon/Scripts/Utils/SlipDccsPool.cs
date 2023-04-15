using UnityEngine.AddressableAssets;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;
using RoR2.Items;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using RoR2.ExpansionManagement;
using UnityEngine.Serialization;
using System.Linq;
using FBLStage.Content;

namespace FBLStage.Utils
{

    [CreateAssetMenu(menuName = "Slipstream/SlipDccsPool")]
    public class SlipDccsPool : DccsPool
    {
        public void ResolveAddressableCategories()
        {
			ResolvePoolConfig();
            poolCategories = new Category[addressablePoolCategories.Length];
            for (int i = 0; i < poolCategories.Length; i++)
            {
                poolCategories[i] = addressablePoolCategories[i].ToCategory();
            }
        }

		//A bit gross to do it here but it doesn't matter, it is a stand alone stage. I will clean this up if needed though.
		public void ResolvePoolConfig()
		{
			//Accessing "Standard" Category"
			AddressableCategory[] cat = FBLContent.monsterDP.addressablePoolCategories;
			if (cat.Length > 0)
            {
				if(cat[0].includedIfConditionsMet.Length > 0) cat[0].includedIfConditionsMet[0].dccs = FBLContent.validDLCPool;
				if (cat[0].includedIfNoConditionsMet.Length > 0) cat[0].includedIfNoConditionsMet[0].dccs = FBLContent.validNormalPool;
			}
		}

		[SerializeField]
        private AddressableCategory[] addressablePoolCategories;

		[Serializable]
		public class AddressablePoolEntry : DccsPool.PoolEntry, IAddressableKeyProvider<DirectorCardCategorySelection>
		{
			[Tooltip("An optional addressable key to load a vanilla dccs")]
			public string dccsKey;
			public string Key => dccsKey;
			public DirectorCardCategorySelection Addressable { set => dccs = value; }
		}

		[Serializable]
		public class AddressableConditionalPoolEntry : DccsPool.ConditionalPoolEntry, IAddressableKeyProvider<DirectorCardCategorySelection>
		{
			[Tooltip("An optional addressable key to load a vanilla dccs")]
			public string dccsKey;
			[Tooltip("Should the Survivors of the Void expansion be added to this entry's required expansions?")]
			public bool requiresDLC1Expansion;
			public string Key => dccsKey;
			public DirectorCardCategorySelection Addressable { set => dccs = value; }
			public void AppendExpansion(ExpansionDef expansion)
			{
				Log.Info($"{this} appending expansiondef {expansion.nameToken}");
				if (requiredExpansions == default(ExpansionDef[]))
				{
					requiredExpansions = Array.Empty<ExpansionDef>();
				}
				HG.ArrayUtils.ArrayAppend(ref requiredExpansions, expansion);
			}
		}

		[Serializable]
		public class AddressableCategory : ISerializationCallbackReceiver
		{
			public static ExpansionDef DLC1
            {
				get => _DLC1 ?? (_DLC1 = Addressables.LoadAssetAsync<ExpansionDef>("RoR2/DLC1/Common/DLC1.asset").WaitForCompletion());
            }
			private static ExpansionDef _DLC1;
			public DccsPool.Category ToCategory()
			{
				if (!hasResolved)
				{
					foreach (IAddressableKeyProvider<DirectorCardCategorySelection> provider in alwaysIncluded)
					{
						provider.Resolve();
					}
					foreach (IAddressableKeyProvider<DirectorCardCategorySelection> provider in includedIfConditionsMet)
					{
						provider.Resolve();
					}
					foreach (AddressableConditionalPoolEntry entry in includedIfConditionsMet)
					{
                        if (entry.requiresDLC1Expansion)
                        {
							entry.AppendExpansion(DLC1);
                        }
					}
					foreach (IAddressableKeyProvider<DirectorCardCategorySelection> provider in includedIfNoConditionsMet)
					{
						provider.Resolve();
					}
					hasResolved = true;
				}
				return new DccsPool.Category
				{
					name = name,
					categoryWeight = categoryWeight,
					alwaysIncluded = alwaysIncluded,
					includedIfConditionsMet = includedIfConditionsMet,
					includedIfNoConditionsMet = includedIfNoConditionsMet,
					hasBeenSerialized = hasBeenSerialized
				};
			}
			public void OnBeforeSerialize()
			{
				this.hasBeenSerialized = true;
			}

			public void OnAfterDeserialize()
			{
				if (!this.hasBeenSerialized)
				{
					this.categoryWeight = 1f;
				}
			}

			[Tooltip("A name to help identify this addressable category")]
			public string name;

			[Tooltip("The weight of all entries in this addressable category relative to the sibling categories.")]
			public float categoryWeight = 1f;

			[Tooltip("These addressable entries are always considered.")]
			public AddressablePoolEntry[] alwaysIncluded;

			[Tooltip("These addressable entries are only considered if their individual conditions are met.")]
			public AddressableConditionalPoolEntry[] includedIfConditionsMet;

			[Tooltip("These addressable entries are considered only if no entries from 'includedIfConditionsMet' have been included.")]
			public AddressablePoolEntry[] includedIfNoConditionsMet;

			[HideInInspector]
			[SerializeField]
			protected bool hasBeenSerialized;

			private bool hasResolved;
		}
	}
}
