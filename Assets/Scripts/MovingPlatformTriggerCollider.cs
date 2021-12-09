using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTriggerCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject movingPlatform;

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
        //Debug.Log("AddPlayer");
        movingPlatform.GetComponent<MovingPlatform>().AddElementToPlayerList(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        movingPlatform.GetComponent<MovingPlatform>().RemoveElementFromPlayerList(other.gameObject);
    }
}
