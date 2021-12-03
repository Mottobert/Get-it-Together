using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderObstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject obstacle;
    [SerializeField]
    private bool right;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fire" || other.tag == "water")
        {
            obstacle.GetComponent<OneWayObstacle>().CheckRelease(right, other.gameObject.tag);
        }
    }
}
