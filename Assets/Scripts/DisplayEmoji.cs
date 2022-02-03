using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Photon.Pun;
using UnityEngine;

public class DisplayEmoji : MonoBehaviour
{
    [SerializeField]
    private int emojiIndex;
    [SerializeField]
    private DisplayEmojiController emojiController;

    [SerializeField]
    private bool playSound = true;

    private GameObject uiAudioManager;

    private void Start()
    {
        uiAudioManager = GameObject.FindGameObjectWithTag("music");
    }

    public void ShowEmojiRPC()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        gameObject.transform.GetComponentInParent<PhotonView>().RPC("DisplayEmojiForAll", RpcTarget.All, emojiController.name, emojiIndex);
        //Debug.Log(gameObject.name);
    }

    public void ShowEmoji()
    {
        emojiController.DisplayEmojiForPlayers(emojiIndex);
    }
}
