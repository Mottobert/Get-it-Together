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

    private GameObject finish;

    public GameObject puzzleManager;

    [SerializeField]
    private HintManager hintManager;

    [SerializeField]
    private float deactivateDelay;

    private void Awake()
    {
        //gameObject.name = GetInstanceID().ToString(); // Sollte aktiviert werden, wenn das Spiel final gebuilded wird
        finish = GameObject.FindGameObjectWithTag("finish");
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

        if (hintManager)
        {
            hintManager.ActivateHintObjects();
        }

        if (finish)
        {
            finish.GetComponent<Finish>().CheckFinishRequirements();
        }

        if (deactivateDelay != 0)
        {
            Invoke("DeactivateFlame", deactivateDelay);
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
        if (hintManager)
        {
            hintManager.DeactivateHintObjects();
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
