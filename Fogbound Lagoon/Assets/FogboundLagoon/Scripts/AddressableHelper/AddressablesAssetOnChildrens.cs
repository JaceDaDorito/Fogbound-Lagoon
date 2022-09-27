using System.Linq;
using System.Reflection;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesHelper
{
    [ExecuteAlways]
    public class AddressablesAssetOnChildrens : MonoBehaviour
    {
        public string Key;
        private Object _asset;

        private void Awake()
        {
            Refresh();
        }

        public void Refresh()
        {
            _asset = Addressables.LoadAssetAsync<Object>(Key).WaitForCompletion();
            if (!_asset)
            {
                Debug.LogError($"AddressablesAsset failed loading {Key}");
                return;
            }

            foreach (var item in gameObject.GetComponentsInChildren<SurfaceDefProvider>())
            {
                item.surfaceDef = (SurfaceDef)_asset;
            }

            Debug.Log($"AddressablesAssetOnChildrens done for {gameObject.name}: {Key}");
        }

        private void OnValidate()
        {
            Refresh();
        }
    }
}
