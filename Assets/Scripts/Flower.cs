using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    private GameObject[] flowerObjects;
    [SerializeField]
    private GameObject[] ladderObjects;

    private void Awake()
    {
        //gameObject.name = GetInstanceID().ToString();
    }

    private void ChangeObjectStatus(GameObject[] objects, bool status)
    {
        foreach(GameObject g in objects)
        {
            g.SetActive(status);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView PVPlayer = other.gameObject.GetComponent<PhotonView>();

        if(other.tag == "water" && PVPlayer.IsMine)
        {
            //ActivateLadder();
            PVPlayer.RPC("ActivateFlowerForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
        else if(other.tag == "fire" && PVPlayer.IsMine)
        {
            //DeactivateLadder();
            PVPlayer.RPC("DeactivateFlowerForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
    }

    public void ActivateLadder()
    {
        ChangeObjectStatus(flowerObjects, false);
        ChangeObjectStatus(ladderObjects, true);
    }

    public void DeactivateLadder()
    {
        ChangeObjectStatus(flowerObjects, true);
        ChangeObjectStatus(ladderObjects, false);
    }
}
