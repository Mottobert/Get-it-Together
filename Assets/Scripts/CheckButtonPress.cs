using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckButtonPress : MonoBehaviour
{
    [SerializeField]
    private KeyCode key;

    [SerializeField]
    private Image image;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key))
        {
            image.color = new Color32(135, 135, 135, 255);
        }
        else if(!Input.GetKey(key))
        {
            image.color = new Color32(255, 255, 255, 255);
        }
    }
}
