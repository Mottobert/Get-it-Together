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
            if (h.GetComponent<Fackel>())
            {
                h.GetComponent<Fackel>().ActivateFlame();
            }
            else
            {
                h.SetActive(true);
            }
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
    }
}
