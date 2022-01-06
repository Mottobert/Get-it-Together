using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameMaterialNoiseOffset : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        float offset = Random.Range(1, 1000);
        this.gameObject.GetComponent<MeshRenderer>().material.SetFloat("Noise Offset", offset);
        Debug.Log(this.gameObject.GetComponent<MeshRenderer>().material.GetFloat("Noise Offset"));
    }
}
