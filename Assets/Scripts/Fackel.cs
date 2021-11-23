using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fackel : MonoBehaviour
{
    [SerializeField]
    private GameObject flame;
    [SerializeField]
    private GameObject pointLight;
    public bool activeFlame = false;

    [SerializeField]
    private GameObject connectedFackel;
    [SerializeField]
    private bool isConnectedFackel;

    public GameObject puzzleManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fire" && !isConnectedFackel)
        {
            ActivateFlame();
        } else if(other.tag == "water" && !isConnectedFackel)
        {
            DeactivateFlame();
        }
    }

    private void ActivateFlame()
    {
        flame.SetActive(true);
        pointLight.SetActive(true);
        activeFlame = true;
        connectedFackel.GetComponent<Fackel>().ActivateConnectedFlame();
        puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
    }

    private void DeactivateFlame()
    {
        flame.SetActive(false);
        pointLight.SetActive(false);
        activeFlame = false;
        connectedFackel.GetComponent<Fackel>().DeactivateConnectedFlame();
        puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
    }


    private void ActivateConnectedFlame()
    {
        flame.SetActive(true);
        pointLight.SetActive(true);
    }

    private void DeactivateConnectedFlame()
    {
        flame.SetActive(false);
        pointLight.SetActive(false);
    }
}
