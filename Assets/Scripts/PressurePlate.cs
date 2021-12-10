using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private GameObject movingPlatform;

    [SerializeField]
    private GameObject plateObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "water" || other.tag == "fire" || other.tag == "iceblock")
        {
            PhotonView PVPlayer = other.gameObject.GetComponentInParent<PhotonView>();

            //ActivatePressurePlate();
            PVPlayer.RPC("ActivatePressurePlateForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "water" || other.tag == "fire" || other.tag == "iceblock")
        {
            PhotonView PVPlayer = other.gameObject.GetComponentInParent<PhotonView>();

            //DeactivatePressurePlate();
            PVPlayer.RPC("DeactivatePressurePlateForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
    }

    public void ActivatePressurePlate()
    {
        plateObject.transform.position = new Vector3(plateObject.transform.position.x, - 0.02f, plateObject.transform.position.z);
        movingPlatform.GetComponent<MovingPlatform>().ActivateMovingPlatform();
    }

    public void DeactivatePressurePlate()
    {
        plateObject.transform.position = new Vector3(plateObject.transform.position.x, 0.05f, plateObject.transform.position.z);
        movingPlatform.GetComponent<MovingPlatform>().DeactivateMovingPlatform();
    }
}
