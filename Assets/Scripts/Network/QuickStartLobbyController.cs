using MoreMountains.Feedbacks;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickStartButton;
    [SerializeField]
    private GameObject quickCancelButton;
    [SerializeField]
    private int roomSize;

    [SerializeField]
    private Slider playerCharacterSlider;

    [SerializeField]
    private bool playSound = true;

    private GameObject uiAudioManager;

    //private int quickStartSpawnMode = 1;

    private void Start()
    {
        uiAudioManager = GameObject.FindGameObjectWithTag("music");

        if(PlayerPrefs.GetString("playerCharacter") == "fire")
        {
            playerCharacterSlider.value = 0;
        }
        else
        {
            playerCharacterSlider.value = 1;
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.SendRate = 30; // Changed
        PhotonNetwork.SerializationRate = 15; // Changed
        PhotonNetwork.AutomaticallySyncScene = true;
        quickStartButton.SetActive(true);
    }

    public void QuickStart()
    {
        if(playerCharacterSlider.value == 0)
        {
            PlayerPrefs.SetString("playerCharacter", "fire");
            Debug.Log("Fire");
        }
        else
        {
            PlayerPrefs.SetString("playerCharacter", "water");
            Debug.Log("Water");
        }

        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        //PlayerPrefs.SetInt("SpawnMode", quickStartSpawnMode);
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Quick start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    public void CreateRoom()
    {
        Debug.Log("Creating room now");
        int randomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("Room" + randomNumber, roomOps);
        Debug.Log(randomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom();
    }

    public void QuickCancel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }
}
