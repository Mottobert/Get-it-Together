using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField]
    private GameObject fire;

    [SerializeField]
    private new Collider collider;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fire")
        {
            DeactivateWood();
        } 
        else if(other.tag == "water")
        {
            ActivateWood();
        }
    }

    private void ActivateWood()
    {
        fire.SetActive(false);
        collider.enabled = true;
    }

    private void DeactivateWood()
    {
        fire.SetActive(true);
        collider.enabled = false;
    }
}
