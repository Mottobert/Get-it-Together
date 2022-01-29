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

    [SerializeField]
    private GameObject finish;

    public GameObject puzzleManager;

    private void Awake()
    {
        //gameObject.name = GetInstanceID().ToString(); // Sollte aktiviert werden, wenn das Spiel final gebuilded wird
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView PVPlayer = other.gameObject.GetComponent<PhotonView>();

        if (other.tag == "fire" && !isConnectedFackel && PVPlayer.IsMine)
        {
            //ActivateFlame();
            PVPlayer.RPC("ActivateFackelForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
            //Debug.Log("Activate Fackel for All");
        } 
        else if(other.tag == "water" && !isConnectedFackel && PVPlayer.IsMine)
        {
            //DeactivateFlame();
            PVPlayer.RPC("DeactivateFackelForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
            //Debug.Log("Deactivate Fackel for All");

        }
    }

    public void ActivateFlame()
    {
        flame.SetActive(true);
        //Debug.Log("Activate Flame");
        pointLight.SetActive(true);
        activeFlame = true;

        if (this.gameObject.GetComponent<AudioSource>())
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }

        if (connectedFackel)
        {
            connectedFackel.GetComponent<Fackel>().ActivateConnectedFlame();
        }

        if (puzzleManager)
        {
            puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
        }

        if (finish)
        {
            finish.GetComponent<Finish>().CheckFinishRequirements();
        }
    }

    public void DeactivateFlame()
    {
        flame.SetActive(false);
        //Debug.Log("Deactivate Flame");
        pointLight.SetActive(false);
        activeFlame = false;

        if (this.gameObject.GetComponent<AudioSource>())
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
        }

        if (connectedFackel)
        {
            connectedFackel.GetComponent<Fackel>().DeactivateConnectedFlame();
        }
        if (puzzleManager)
        {
            puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
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
