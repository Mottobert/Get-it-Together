using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private int finishedPlayers = 0;
    [SerializeField]
    private GameObject flag;
    [SerializeField]
    private ParticleSystem finishParticleSystem;

    [SerializeField]
    private GameObject[] finishRequirements;

    public LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "playerFinishCollider")
        {
            finishedPlayers++;
        }

        if (finishedPlayers == 2)
        {
            if (CheckFinishRequirements())
            {
                Debug.Log("Finished");
                flag.SetActive(true);
                finishParticleSystem.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "fire" || other.gameObject.tag == "water")
        {
            finishedPlayers--;
        }

        if (finishedPlayers < 0)
        {
            finishedPlayers = 0;
        }
    }

    private bool CheckFinishRequirements()
    {
        foreach(GameObject g in finishRequirements)
        {
            if (g.GetComponent<Fackel>() && !g.GetComponent<Fackel>().activeFlame || g.GetComponent<Waterfall>() && !g.GetComponent<Waterfall>().activeWaterfall)
            {
                return false;
            }
        }
        return true;
    }
}
