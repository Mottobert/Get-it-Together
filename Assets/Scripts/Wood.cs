using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField]
    private GameObject fire;

    [SerializeField]
    private new Collider collider;

    private void Awake()
    {
        //gameObject.name = GetInstanceID().ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView PVPlayer = other.gameObject.GetComponent<PhotonView>();

        if(other.tag == "fire" && PVPlayer.IsMine)
        {
            //DeactivateWood();
            PVPlayer.RPC("DeactivateWoodForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        } 
        else if(other.tag == "water" && PVPlayer.IsMine)
        {
            //ActivateWood();
            PVPlayer.RPC("ActivateWoodForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
    }

    public void ActivateWood()
    {
        fire.SetActive(false);
        collider.enabled = true;
    }

    public void DeactivateWood()
    {
        fire.SetActive(true);
        collider.enabled = false;
    }
}
