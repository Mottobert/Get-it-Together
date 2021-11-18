using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    private GameObject[] flowerObjects;
    [SerializeField]
    private GameObject[] ladderObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeObjectStatus(bool status, GameObject[] objects)
    {
        foreach(GameObject g in objects)
        {
            g.SetActive(status);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "water")
        {
            ChangeObjectStatus(false, flowerObjects);
            ChangeObjectStatus(true, ladderObjects);
        } else if(other.tag == "fire")
        {
            ChangeObjectStatus(true, flowerObjects);
            ChangeObjectStatus(false, ladderObjects);
        }
    }
}
