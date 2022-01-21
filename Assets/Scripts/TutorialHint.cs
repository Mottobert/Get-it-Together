using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHint : MonoBehaviour
{
    [SerializeField]
    private GameObject hintPanel;

    public void ShowHint()
    {
        hintPanel.GetComponent<Animator>().SetBool("activate", true);
    }

    public void HideHint()
    {
        hintPanel.GetComponent<Animator>().SetBool("activate", false);
    }
}
