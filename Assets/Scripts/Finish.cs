using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private int finishedPlayers = 0;
    [SerializeField]
    private GameObject flag;

    [SerializeField]
    private GameObject[] finishRequirements;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(finishedPlayers);
        Debug.Log(other.gameObject);
        if(other.gameObject.tag == "fire" || other.gameObject.tag == "water")
        {
            finishedPlayers++;
        }

        if(finishedPlayers == 4)
        {
            if (CheckFinishRequirements())
            {
                Debug.Log("Finished");
                flag.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "fire" || other.tag == "water")
        {
            finishedPlayers--;
        }
    }

    private bool CheckFinishRequirements()
    {
        foreach(GameObject g in finishRequirements)
        {
            if (!g.GetComponent<Fackel>().activeFlame)
            {
                return false;
            }
        }
        return true;
    }
}
