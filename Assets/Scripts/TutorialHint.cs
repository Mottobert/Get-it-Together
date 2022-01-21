using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHint : MonoBehaviour
{
    [SerializeField]
    private GameObject hintObject;

    public void ShowHint()
    {
        hintObject.SetActive(true);
    }

    public void HideHint()
    {
        hintObject.SetActive(false);
    }
}
