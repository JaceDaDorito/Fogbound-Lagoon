using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesHelper
{
    public class InjectRoR2AssetEditorPreview : MonoBehaviour
    {
        public string Key;

        private GameObject instance;

        public bool _refreshInEditor;

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


            instance = Instantiate(Addressables.LoadAssetAsync<GameObject>(Key).WaitForCompletion(), gameObject.transform);
        }
    }
}
