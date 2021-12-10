using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTriggerCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject movingPlatform;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("AddPlayer");
        if(other.tag == "platformMovable")
        {
            movingPlatform.GetComponent<MovingPlatform>().AddElementToPlayerList(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "platformMovable")
        {
            movingPlatform.GetComponent<MovingPlatform>().RemoveElementFromPlayerList(other.gameObject);
        }
    }
}
