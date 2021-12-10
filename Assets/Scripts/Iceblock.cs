using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceblock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fire")
        {
            Destroy(this.gameObject);
        }
    }

    // Pressure Plate
    [PunRPC]
    public void ActivatePressurePlateForAll(string name)
    {
        //Debug.Log("Activate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<PressurePlate>().ActivatePressurePlate();
    }

    [PunRPC]
    public void DeactivatePressurePlateForAll(string name)
    {
        //Debug.Log("Deactivate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<PressurePlate>().DeactivatePressurePlate();
    }
}
