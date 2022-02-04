using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hintObjects;

    [SerializeField]
    private GameObject activeEmoji;
    [SerializeField]
    private ParticleSystem emojiParticleSystem;

    public void ActivateHintObjects()
    {
        foreach(GameObject h in hintObjects)
        {
            if (h.GetComponent<Fackel>())
            {
                h.GetComponent<Fackel>().ActivateFlame();
            }
            else
            {
                h.SetActive(true);
            }

            activeEmoji.SetActive(true);
            activeEmoji.GetComponent<Animator>().SetTrigger("activateAnimation");
            emojiParticleSystem.Play();
        }
    }

    public void DeactivateHintObjects()
    {
        foreach (GameObject h in hintObjects)
        {
            if (h.GetComponent<Fackel>())
            {
                h.GetComponent<Fackel>().DeactivateFlame();
            }
            else
            {
                h.SetActive(false);
            }
        }

        activeEmoji.SetActive(false);
        activeEmoji.GetComponent<Animator>().SetTrigger("activateAnimation");
    }
}
