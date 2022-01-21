using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceblock : MonoBehaviour
{
    private void Update()
    {
        this.gameObject.transform.rotation = Quaternion.identity;
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
    }

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
