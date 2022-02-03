using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using MoreMountains.Feedbacks;

public class CustomMatchmakingRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiplayerSceneIndex;

    [SerializeField]
    private GameObject lobbyPanel;
    [SerializeField]
    private GameObject roomPanel;

    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private Transform playersContainer;
    [SerializeField]
    private GameObject playerListingPrefab;

    [SerializeField]
    private TextMeshProUGUI roomNameDisplay;

    [SerializeField]
    private bool playSound = true;

    private GameObject uiAudioManager;

    private void Start()
    {
        uiAudioManager = GameObject.FindGameObjectWithTag("music");
    }

    void ClearPlayerListings()
    {
        for (int i = playersContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(playersContainer.GetChild(i).gameObject);
        }
    }

    void ListPlayers()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            GameObject tempListing = Instantiate(playerListingPrefab, playersContainer);
            TextMeshProUGUI tempText = tempListing.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            tempText.text = player.NickName;
        }
    }

    public override void OnJoinedRoom()
    {
        roomPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        roomNameDisplay.text = PhotonNetwork.CurrentRoom.Name;
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }

        ClearPlayerListings();
        ListPlayers();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        ClearPlayerListings();
        ListPlayers();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        ClearPlayerListings();
        ListPlayers();
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }

IEnumerator rejoinLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }

    public void BackOnClick()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(rejoinLobby());
    }
}
