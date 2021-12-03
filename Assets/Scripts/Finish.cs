using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private int finishedPlayers = 0;
    [SerializeField]
    private GameObject flag;
    [SerializeField]
    private ParticleSystem finishParticleSystem;

    [SerializeField]
    private GameObject[] finishRequirements;

    public LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        PhotonView PVPlayer = other.gameObject.GetComponent<PhotonView>();

        if (other.gameObject.tag == "playerFinishCollider")
        {
            finishedPlayers++;
        }

        if (finishedPlayers == 2)
        {
            if (CheckFinishRequirements() && PVPlayer.IsMine)
            {
                //LevelFinished();
                PVPlayer.RPC("LevelFinishedForAll", RpcTarget.All, gameObject.name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "fire" || other.gameObject.tag == "water")
        {
            finishedPlayers--;
        }

        if (finishedPlayers < 0)
        {
            finishedPlayers = 0;
        }
    }

    private bool CheckFinishRequirements()
    {
        foreach(GameObject g in finishRequirements)
        {
            if (g.GetComponent<Fackel>() && !g.GetComponent<Fackel>().activeFlame || g.GetComponent<Waterfall>() && !g.GetComponent<Waterfall>().activeWaterfall)
            {
                return false;
            }
        }
        return true;
    }

    public void LevelFinished()
    {
        Debug.Log("Finished");
        flag.SetActive(true);
        finishParticleSystem.Play();
    }
}
