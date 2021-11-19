using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    [SerializeField]
    private Material waterfallEmptyMaterial;
    [SerializeField]
    private Material waterfallFlowingMaterial;
    [SerializeField]
    private GameObject waterfallObject;

    public bool activeWaterfall = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "water")
        {
            ActivateWaterfall();
        }
        else
        {
            DeactivateWaterfall();
        }
    }

    private void ActivateWaterfall()
    {
        waterfallObject.GetComponent<MeshRenderer>().material = waterfallFlowingMaterial;
        activeWaterfall = true;
    }

    private void DeactivateWaterfall()
    {
        waterfallObject.GetComponent<MeshRenderer>().material = waterfallEmptyMaterial;
        activeWaterfall = false;
    }
}
