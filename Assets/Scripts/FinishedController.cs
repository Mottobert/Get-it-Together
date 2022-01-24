using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishedController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeLabel;
    [SerializeField]
    private TextMeshProUGUI commentLabel;
    [SerializeField]
    private GameObject[] stars;

    private TimeLimit timeLimitData;
    private float usedTime;
    

    // Start is called before the first frame update
    void Start()
    {
        timeLimitData = GameObject.Find("TimeLimit").GetComponent<TimeLimit>();
    }

    public void ShowFinishedCard()
    {
        usedTime = Time.timeSinceLevelLoad;

        for (int i = 0; i < 4; i++)
        {
            if (usedTime <= timeLimitData.timeLimits[i])
            {
                commentLabel.text = timeLimitData.comments[i];
                timeLabel.text = "Ihr habt das Ziel in " + ConvertSecondsToMinutes(usedTime) + " Minuten erreicht.";

                for (int j = 2; j > i - 1; j--)
                {
                    stars[j].SetActive(true);
                }

                return;
            }
        }
    }

    private string ConvertSecondsToMinutes(float timer)
    {
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.RoundToInt(timer % 60);

        string newMinutes = "";
        string newSeconds = "";

        if (minutes < 10)
        {
            newMinutes = "0" + minutes.ToString();
        }
        else
        {
            newMinutes = minutes.ToString();
        }
        if (seconds < 10)
        {
            newSeconds = "0" + Mathf.RoundToInt(seconds).ToString();
        }
        else
        {
            newSeconds = Mathf.RoundToInt(seconds).ToString();
        }

        return newMinutes + ":" + newSeconds;
    }
}
