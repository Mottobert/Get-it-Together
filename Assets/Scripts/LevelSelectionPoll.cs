using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionPoll : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttons;

    public GameObject lastButton;

    public void CheckAllButtons()
    {
        foreach(GameObject b in buttons)
        {
            if(b.GetComponent<LevelSelectionButton>().votes == 2 && PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(b.GetComponent<LevelSelectionButton>().sceneIndex);
            }
        }
    }

    public void LastButtonVoteDown()
    {
        lastButton.GetComponent<LevelSelectionButton>().VoteDownRPC();
    }
}
