using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hintObjects;

    public void ActivateHintObjects()
    {
        foreach(GameObject h in hintObjects)
        {
            h.GetComponent<Fackel>().ActivateFlame();
        }
    }

    public void DeactivateHintObjects()
    {
        foreach (GameObject h in hintObjects)
        {
            h.GetComponent<Fackel>().DeactivateFlame();
        }
    }
}
