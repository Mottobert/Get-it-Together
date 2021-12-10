using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    public int votes = 0;


    public void ButtonClicked()
    {
        if (levelSelectionPoll.lastButton)
        {
            levelSelectionPoll.LastButtonVoteDown();
        }

        playerPV.RPC("VoteUpForAll", RpcTarget.AllBufferedViaServer, gameObject.name);

        levelSelectionPoll.lastButton = this.gameObject;
    }

    public void VoteUp()
    {
        votes++;
        votesText.text = "" + votes;

        levelSelectionPoll.CheckAllButtons();
    }

    public void VoteDownRPC()
    {
        playerPV.RPC("VoteDownForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
    }

    public void VoteDown()
    {
        votes--;
        votesText.text = "" + votes;
    }
}
