using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fackel : MonoBehaviour
{
    [SerializeField]
    private GameObject flame;
    public bool activeFlame = false;

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
        if(other.tag == "fire")
        {
            ActivateFlame();
        } else if(other.tag == "water")
        {
            DeactivateFlame();
        }
    }

    private void ActivateFlame()
    {
        flame.SetActive(true);
        activeFlame = true;
    }

    private void DeactivateFlame()
    {
        flame.SetActive(false);
        activeFlame = false;
    }
}
