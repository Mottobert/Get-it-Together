using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHintTrigger : MonoBehaviour
{
    [SerializeField]
    private TutorialHint tutorialHint;

    private void OnTriggerEnter(Collider other)
    {
        tutorialHint.ShowHint();
    }

    private void OnTriggerExit(Collider other)
    {
        tutorialHint.HideHint();
    }
}
