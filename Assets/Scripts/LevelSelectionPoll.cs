using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionPoll : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttons;

    public GameObject lastButton;

    private GameObject votedButton;

    public void CheckAllButtons()
    {
        foreach(GameObject b in buttons)
        {
            if(b.GetComponent<LevelSelectionButton>().votes == 2 && PhotonNetwork.IsMasterClient)
            {
                votedButton = b;
                Invoke("OpenLevel", 0.3f);
            }
        }
    }

    private void OpenLevel()
    {
        PhotonNetwork.LoadLevel(votedButton.GetComponent<LevelSelectionButton>().sceneIndex);
    }

    public void LastButtonVoteDown()
    {
        lastButton.GetComponent<LevelSelectionButton>().VoteDownRPC();
    }
}
