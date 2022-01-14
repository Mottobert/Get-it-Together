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


    public void ButtonClicked()
    {
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
        //votesText.text = "" + votes + "/2";

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
        //votesText.text = "" + votes + "/2";

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
            //.LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
