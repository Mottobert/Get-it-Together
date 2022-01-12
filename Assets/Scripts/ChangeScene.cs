using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private int sceneIndex;
    [SerializeField]
    private bool disconnectOnSceneChange;
    [SerializeField]
    private bool nextLevel;

    public void ChangeSceneOnClick()
    {
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
