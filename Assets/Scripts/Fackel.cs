using Photon.Pun;
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

    [SerializeField]
    private GameObject connectedFackel;
    [SerializeField]
    private bool isConnectedFackel;

    public GameObject puzzleManager;

    [SerializeField]
    private GameObject movingPlatform;

    private void Awake()
    {
        //gameObject.name = GetInstanceID().ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView PVPlayer = other.gameObject.GetComponent<PhotonView>();

        if (other.tag == "fire" && !isConnectedFackel && PVPlayer.IsMine)
        {
            //ActivateFlame();
            PVPlayer.RPC("ActivateFackelForAll", RpcTarget.All, gameObject.name);
        } 
        else if(other.tag == "water" && !isConnectedFackel && PVPlayer.IsMine)
        {
            //DeactivateFlame();
            PVPlayer.RPC("DeactivateFackelForAll", RpcTarget.All, gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PhotonView PVPlayer = other.gameObject.GetComponent<PhotonView>();

        if (movingPlatform && other.tag == "fire" && PVPlayer.IsMine)
        {
            PVPlayer.RPC("DeactivateFackelForAll", RpcTarget.All, gameObject.name);
        }
    }

    public void ActivateFlame()
    {
        flame.SetActive(true);
        pointLight.SetActive(true);
        activeFlame = true;
        if (connectedFackel)
        {
            connectedFackel.GetComponent<Fackel>().ActivateConnectedFlame();
        }
        if (puzzleManager)
        {
            puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
        }

        if (movingPlatform)
        {
            movingPlatform.GetComponent<MovingPlatform>().ActivateMovingPlatform();
        }
    }

    public void DeactivateFlame()
    {
        flame.SetActive(false);
        pointLight.SetActive(false);
        activeFlame = false;
        if (connectedFackel)
        {
            connectedFackel.GetComponent<Fackel>().DeactivateConnectedFlame();
        }
        if (puzzleManager)
        {
            puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
        }

        if (movingPlatform)
        {
            movingPlatform.GetComponent<MovingPlatform>().DeactivateMovingPlatform();
        }
    }


    private void ActivateConnectedFlame()
    {
        flame.SetActive(true);
        pointLight.SetActive(true);
    }

    private void DeactivateConnectedFlame()
    {
        flame.SetActive(false);
        pointLight.SetActive(false);
    }
}
