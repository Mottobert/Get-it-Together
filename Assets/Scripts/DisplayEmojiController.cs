using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayEmojiController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] emojis;
    [SerializeField]
    private InputController inputController;
    [SerializeField]
    private ParticleSystem poofPS;
    [SerializeField]
    private GameObject poofAnimation;

    private int activeEmojiIndex;

    private bool active;

    public void DisplayEmojiForPlayers(int index)
    {
        if (active)
        {
            DisableActiveEmoji();
            poofPS.Stop();
            poofAnimation.GetComponent<Animator>().SetBool("activateAnimation", false);
            this.gameObject.GetComponent<AudioSource>().Play();
            StopAllCoroutines();
        }

        active = true;
        activeEmojiIndex = index;
        emojis[activeEmojiIndex].SetActive(true);
        if (emojis[activeEmojiIndex].GetComponent<Animator>())
        {
            emojis[activeEmojiIndex].GetComponent<Animator>().SetTrigger("activateAnimation");
            poofPS.Play();
            poofAnimation.GetComponent<Animator>().SetBool("activateAnimation", true);
        }

        inputController.DeactivateKommunikationPanel();

        //Invoke("DisableActiveEmoji", 3f);
        StartCoroutine(DeactivateEmoji());
    }

    private void DisableActiveEmoji()
    {
        active = false;
        emojis[activeEmojiIndex].SetActive(false);
    }

    IEnumerator DeactivateEmoji()
    {
        yield return new WaitForSeconds(3);
        DisableActiveEmoji();
        poofPS.Stop();
        poofAnimation.GetComponent<Animator>().SetBool("activateAnimation", false);
    }
}
