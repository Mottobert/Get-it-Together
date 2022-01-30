using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI animatedText;

    private string dots = "";
    private int iteration = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.frameCount % 20 == 0)
        {
            if (iteration == 0)
            {
                dots = "";    
            }
            else if (iteration == 1)
            {
                dots = ".";
            }
            else if (iteration == 2)
            {
                dots = "..";
            }
            else if (iteration == 3)
            {
                dots = "...";
            }
            else if (iteration >= 4)
            {
                iteration = -1;
            }

            iteration++;

            animatedText.text = "Laden" + dots;
        }
    }
}
