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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.frameCount % 10 == 0)
        {
            if(iteration < 3)
            {
                iteration++;
            }
            else if(iteration >= 3)
            {
                iteration = 0;
            }

            for(int i = 0; i < iteration; i++)
            {
                dots += ".";
            }
            
            animatedText.text = "Laden" + dots;
        }
    }
}
