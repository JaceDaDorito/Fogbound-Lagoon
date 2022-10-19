using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using AddressablesHelper;

public class InjectedAssetToggleSync : MonoBehaviour
{
    public AddressablePrefab _injectedAssetComponent;

    private GameObject instance;

    private void OnEnable()
    {
        Refresh();
    }

    private void OnDisable()
    {
        Refresh();
    }

    private void Refresh()
    {
        if (!_injectedAssetComponent)
            return;
        instance = _injectedAssetComponent.GetInstance();
        if (instance)
        {
            instance.SetActive(gameObject.activeSelf);
        }
    }
}
