using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayEmojiController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] emojis;
    [SerializeField]
    private InputController inputController;

    private int activeEmojiIndex;

    private bool active;

    public void DisplayEmojiForPlayers(int index)
    {
        if (!active)
        {
            active = true;
            activeEmojiIndex = index;
            emojis[activeEmojiIndex].SetActive(true);

            inputController.DeactivateKommunikationPanel();

            Invoke("DisableActiveEmoji", 3f);
        }
        
    }

    private void DisableActiveEmoji()
    {
        active = false;
        emojis[activeEmojiIndex].SetActive(false);
    }
}
