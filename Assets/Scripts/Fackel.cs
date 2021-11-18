using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fackel : MonoBehaviour
{
    [SerializeField]
    private GameObject flame;
    [SerializeField]
    private GameObject pointLight;
    public bool activeFlame = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fire")
        {
            ActivateFlame();
        } else if(other.tag == "water")
        {
            DeactivateFlame();
        }
    }

    private void ActivateFlame()
    {
        flame.SetActive(true);
        pointLight.SetActive(true);
        activeFlame = true;
    }

    private void DeactivateFlame()
    {
        flame.SetActive(false);
        pointLight.SetActive(false);
        activeFlame = false;
    }
}
