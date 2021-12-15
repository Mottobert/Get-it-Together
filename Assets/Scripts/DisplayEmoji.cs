using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DisplayEmoji : MonoBehaviour
{
    [SerializeField]
    private int emojiIndex;
    [SerializeField]
    private DisplayEmojiController emojiController;

    public void ShowEmojiRPC()
    {
        gameObject.transform.GetComponentInParent<PhotonView>().RPC("DisplayEmojiForAll", RpcTarget.All, emojiController.name, emojiIndex);
        //Debug.Log(gameObject.name);
    }

    public void ShowEmoji()
    {
        emojiController.DisplayEmojiForPlayers(emojiIndex);
    }
}
