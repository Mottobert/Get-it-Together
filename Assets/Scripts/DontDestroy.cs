using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField]
    private string dontDestroyTag;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(dontDestroyTag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}