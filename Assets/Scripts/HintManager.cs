using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hintObjects;

    [SerializeField]
    private float deactivateDelay;

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
        Invoke("DeactivateHintObjects", deactivateDelay);
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
