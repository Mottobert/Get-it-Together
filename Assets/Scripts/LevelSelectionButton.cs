using MoreMountains.Feedbacks;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour
{
    public int sceneIndex;

    [SerializeField]
    private LevelSelectionPoll levelSelectionPoll;
    [SerializeField]
    private TextMeshProUGUI votesText;
    [SerializeField]
    private PhotonView playerPV;
    [SerializeField]
    private GameObject waterIcon;
    [SerializeField]
    private GameObject fireIcon;
    [SerializeField]
    public bool nextLevel;

    public int votes = 0;

    [SerializeField]
    private bool playSound = true;

    private GameObject uiAudioManager;

    private void Start()
    {
        uiAudioManager = GameObject.FindGameObjectWithTag("music");
    }

    public void ButtonClicked()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        if (levelSelectionPoll.lastButton)
        {
            levelSelectionPoll.LastButtonVoteDown();
        }

        playerPV.RPC("VoteUpForAll", RpcTarget.AllBufferedViaServer, gameObject.name, PhotonNetwork.IsMasterClient);

        levelSelectionPoll.lastButton = this.gameObject;
    }

    public void VoteUp(bool masterClient)
    {
        votes++;

        if (masterClient)
        {
            fireIcon.SetActive(true);
        } 
        else
        {
            waterIcon.SetActive(true);
        }

        levelSelectionPoll.CheckAllButtons();
    }

    public void VoteDownRPC()
    {
        playerPV.RPC("VoteDownForAll", RpcTarget.AllBufferedViaServer, gameObject.name, PhotonNetwork.IsMasterClient);
    }

    public void VoteDown(bool masterClient)
    {
        votes--;

        if (masterClient)
        {
            fireIcon.SetActive(false);
        }
        else
        {
            waterIcon.SetActive(false);
        }
    }

    public void OpenNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.GetSceneByBuildIndex(nextLevel) != null)
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        } 
        else if(SceneManager.GetSceneByBuildIndex(nextLevel) == null)
        {
            PhotonNetwork.LoadLevel(0);
        }
    }
}
