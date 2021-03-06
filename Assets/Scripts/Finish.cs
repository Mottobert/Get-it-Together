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

    [SerializeField]
    private Animator finishInfoText;

    private bool levelFinished = false;

    [SerializeField]
    private ParticleSystem finishParticleSystem;

    [SerializeField]
    private MMFeedbacks finishParticleEffects;

    [SerializeField]
    private GameObject[] finishRequirements;

    private GameObject[] playersInFinish = new GameObject[2];

    public LayerMask playerLayer;

    [SerializeField]
    private GameObject finishEmoji;
    [SerializeField]
    private GameObject partyEmoji;
    [SerializeField]
    private ParticleSystem emojiParticleSystem;

    private void Start()
    {
        flag.SetActive(true);
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
                //flag.transform.position = flagMid.position; //Vector3.Lerp(flag.transform.position, flagMid.position, 0.01f);
                flag.GetComponent<Animator>().SetBool("halb", true);
                flag.GetComponent<Animator>().SetBool("start", false);
            }

            //Debug.Log(finishedPlayers);

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

            if (finishedPlayers < 1)
            {
                //flag.transform.position = flagStart.position; //Vector3.Lerp(flag.transform.position, flagStart.position, 0.01f);
                flag.GetComponent<Animator>().SetBool("start", true);
                flag.GetComponent<Animator>().SetBool("halb", false);
                finishedPlayers = 0;
            }

            finishInfoText.SetBool("ShowInfoText", false);
        }
    }

    public bool CheckFinishRequirements()
    {
        foreach(GameObject g in finishRequirements)
        {
            if (g.GetComponent<Fackel>() && !g.GetComponent<Fackel>().activeFlame || g.GetComponent<Waterfall>() && !g.GetComponent<Waterfall>().activeWaterfall || g.GetComponent<Puzzle>() && !g.GetComponent<Puzzle>().solved)
            {
                finishInfoText.SetBool("ShowInfoText", true);
                finishEmoji.SetActive(false);

                return false;
            }
        }
        finishInfoText.SetBool("ShowInfoText", false);

        finishEmoji.SetActive(true);
        finishEmoji.GetComponent<Animator>().SetTrigger("activateAnimation");
        //StartCoroutine("DeactivateEmoji", finishEmoji);

        return true;
    }

    public void LevelFinished()
    {
        if (!levelFinished)
        {
            levelFinished = true;
            //flag.transform.position = flagEnd.position; //Vector3.Lerp(flag.transform.position, flagEnd.position, 0.01f);
            flag.GetComponent<Animator>().SetBool("oben", true);
            Debug.Log("Finished");
            flag.SetActive(true);
            finishParticleEffects.PlayFeedbacks();

            DisableActiveEmoji(finishEmoji);
            partyEmoji.SetActive(true);
            partyEmoji.GetComponent<Animator>().SetTrigger("activateAnimation");
            emojiParticleSystem.Play();

            //finishParticleSystem.Play();
        }
    }

    private void DisableActiveEmoji(GameObject emoji)
    {
        emoji.SetActive(false);
    }

    IEnumerator DeactivateEmoji(GameObject emoji)
    {
        yield return new WaitForSeconds(3);
        DisableActiveEmoji(emoji);
    }
}
