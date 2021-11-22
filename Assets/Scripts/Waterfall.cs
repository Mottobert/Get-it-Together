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
    [SerializeField]
    private Transform iceblockSpawnPoint;
    [SerializeField]
    private GameObject iceblockObject;
    private GameObject activeIceblock;

    public bool activeWaterfall = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "water" && !activeWaterfall)
        {
            ActivateWaterfall();
        }
        else if (other.tag == "water" && activeWaterfall && !activeIceblock)
        {
            //activeIceblock = Instantiate(iceblockObject, iceblockSpawnPoint.position, Quaternion.identity);
        }
        else if(other.tag == "fire") { 
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
