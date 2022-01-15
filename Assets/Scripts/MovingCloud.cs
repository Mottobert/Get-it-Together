using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCloud : MonoBehaviour
{
    private float speed;
    [SerializeField]
    private Transform resetPos;

    private void Start()
    {
        speed = Random.Range(0.001f, 0.005f);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + speed, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Reset");
        if(other.tag == "resetBox")
        {
            this.gameObject.transform.position = new Vector3(resetPos.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
    }
}
