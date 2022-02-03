using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using MoreMountains.Feedbacks;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private int sceneIndex;
    [SerializeField]
    private bool disconnectOnSceneChange;
    [SerializeField]
    private bool nextLevel;

    [SerializeField]
    private bool playSound = true;

    private GameObject uiAudioManager;

    private void Start()
    {
        uiAudioManager = GameObject.FindGameObjectWithTag("music");
    }

    public void ChangeSceneOnClick()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }

        if (PhotonNetwork.IsConnected && disconnectOnSceneChange)
        {
            PhotonNetwork.Disconnect();
        }
        if (nextLevel)
        {
            OpenNextLevel();
        }
        else
        {
            SceneManager.LoadScene(sceneBuildIndex: sceneIndex);
        }
    }

    public void ReloadSceneForAll()
    {
        PhotonView PVPlayer = this.gameObject.GetComponentInParent<PhotonView>();

        PVPlayer.RPC("ReloadSceneForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex);
    }

    private void OpenNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.GetSceneByBuildIndex(nextLevel) != null)
        {
            SceneManager.LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
