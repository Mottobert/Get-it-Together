using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceblock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fire")
        {
            Destroy(this.gameObject);
        }
    }
}
