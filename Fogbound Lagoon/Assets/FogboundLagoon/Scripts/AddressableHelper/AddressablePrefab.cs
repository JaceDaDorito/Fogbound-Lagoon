using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using RoR2.Networking;
using UnityEngine.Networking;


namespace AddressablesHelper
{
    public class AddressablePrefab : MonoBehaviour
    {
        public string AssetPath;

        private GameObject instance;

        public bool _refreshInEditor;
        public bool _networkInstantiate;

        public GameObject GetInstance()
        {
            return instance;
        }

        private void OnEnable()
        {
            Refresh();
        }

        private void OnDisable()
        {
            if (instance)
            {
                DestroyImmediate(instance);
            }
        }

        private void OnValidate()
        {
            if (_refreshInEditor)
                Refresh();
        }

        private void Refresh()
        {
            if (instance)
            {
                DestroyImmediate(instance);
            }

            
            if(_networkInstantiate && !Application.isEditor)
            {
                if (NetworkServer.active)
                {
                    instance = Instantiate(Addressables.LoadAssetAsync<GameObject>(AssetPath).WaitForCompletion(), gameObject.transform);
                    NetworkServer.Spawn(instance);
                }
                
            }
            else
            {
                instance = Instantiate(Addressables.LoadAssetAsync<GameObject>(AssetPath).WaitForCompletion(), gameObject.transform);
            }
                

            instance.hideFlags = HideFlags.DontSaveInEditor;
        }
    }
}