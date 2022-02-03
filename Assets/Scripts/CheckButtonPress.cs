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

    private float resetScaleWidth;
    private float resetScaleHeight;
    void Start()
    {
        resetScaleWidth = this.gameObject.GetComponent<RectTransform>().rect.width;
        resetScaleHeight = this.gameObject.GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key))
        {
            image.color = new Color32(135, 135, 135, 255);

            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(resetScaleWidth - 5, resetScaleHeight - 5);
        }
        else if(!Input.GetKey(key))
        {
            image.color = new Color32(255, 255, 255, 255);
            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(resetScaleWidth, resetScaleHeight);
        }
    }
}
