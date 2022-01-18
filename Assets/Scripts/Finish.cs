using MoreMountains.Feedbacks;
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
    private Transform flagStart;
    [SerializeField]
    private Transform flagMid;
    [SerializeField]
    private Transform flagEnd;

    private bool levelFinished = false;

    [SerializeField]
    private ParticleSystem finishParticleSystem;

    [SerializeField]
    private MMFeedbacks finishParticleEffects;

    [SerializeField]
    private GameObject[] finishRequirements;

    private GameObject[] playersInFinish = new GameObject[2];

    public LayerMask playerLayer;

    private void Start()
    {
        flag.SetActive(true);
        flag.transform.position = flagStart.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!levelFinished)
        {
            PhotonView PVPlayer = other.gameObject.transform.GetComponentInParent<PhotonView>();

            if (other.gameObject.tag == "playerFinishCollider")
            {
                playersInFinish[finishedPlayers] = other.gameObject;
                finishedPlayers++;
            }

            if (finishedPlayers == 1 && CheckFinishRequirements())
            {
                flag.transform.position = flagMid.position; //Vector3.Lerp(flag.transform.position, flagMid.position, 0.01f);
            }

            Debug.Log(finishedPlayers);

            if (finishedPlayers == 2)
            {
                if (CheckFinishRequirements() && PVPlayer.IsMine)
                {
                    foreach (GameObject p in playersInFinish)
                    {
                        p.GetComponentInParent<PhotonView>().RPC("LevelFinishedForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!levelFinished)
        {
            if (other.gameObject.tag == "playerFinishCollider")
            {
                finishedPlayers--;
            }

            if (finishedPlayers < 0)
            {
                flag.transform.position = flagStart.position; //Vector3.Lerp(flag.transform.position, flagStart.position, 0.01f);
                finishedPlayers = 0;
            }
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
        levelFinished = true;
        flag.transform.position = flagEnd.position; //Vector3.Lerp(flag.transform.position, flagEnd.position, 0.01f);
        Debug.Log("Finished");
        flag.SetActive(true);
        finishParticleEffects.PlayFeedbacks();
        //finishParticleSystem.Play();
    }
}
