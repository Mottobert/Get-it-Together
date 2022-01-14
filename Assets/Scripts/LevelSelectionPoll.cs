using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (votedButton.GetComponent<LevelSelectionButton>().nextLevel)
        {
            if(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1) != null)
            {
                PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                PhotonNetwork.LoadLevel(4);
            }
        }
        else
        {
            PhotonNetwork.LoadLevel(votedButton.GetComponent<LevelSelectionButton>().sceneIndex);
        }
    }

    public void LastButtonVoteDown()
    {
        lastButton.GetComponent<LevelSelectionButton>().VoteDownRPC();
    }
}
