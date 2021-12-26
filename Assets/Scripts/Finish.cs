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

    private GameObject[] playersInFinish = new GameObject[2];

    public LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        PhotonView PVPlayer = other.gameObject.transform.GetComponentInParent<PhotonView>();

        // .Log(other.gameObject);

        if (other.gameObject.tag == "playerFinishCollider")
        {
            playersInFinish[finishedPlayers] = other.gameObject;
            finishedPlayers++;
        }

        Debug.Log(playersInFinish.Length);

        if (finishedPlayers == 2)
        {
            
            if (CheckFinishRequirements() && PVPlayer.IsMine)
            {
                //LevelFinished();
                //PVPlayer.RPC("LevelFinishedForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
                
                foreach(GameObject p in playersInFinish)
                {
                    p.GetComponentInParent<PhotonView>().RPC("LevelFinishedForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
                }
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
