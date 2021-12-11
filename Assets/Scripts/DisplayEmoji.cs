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
        gameObject.transform.GetComponentInParent<PhotonView>().RPC("DipslayEmojiForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
    }

    public void ShowEmoji()
    {
        emojiController.DisplayEmojiForPlayers(emojiIndex);
    }
}
