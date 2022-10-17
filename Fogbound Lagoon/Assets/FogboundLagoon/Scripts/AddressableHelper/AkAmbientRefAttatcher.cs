using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoR2;
using UnityEngine.AddressableAssets;

public class AkAmbientRefAttatcher : MonoBehaviour
{
    //This script is hella scuffed but it does the job

    public AkAmbient component;
    public string key;
    void Awake()
    {
        component.data.WwiseObjectReference = Addressables.LoadAssetAsync<WwiseEventReference>(key).WaitForCompletion();
    }
}
