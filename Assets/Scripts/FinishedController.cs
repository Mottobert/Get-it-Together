using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class FinishedController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeLabel;
    [SerializeField]
    private TextMeshProUGUI commentLabel;

    [SerializeField]
    public GameObject nextLevelButton;
    [SerializeField]
    public GameObject levelButtons;
    [SerializeField]
    private GameObject levelauswahlPanel;

    [SerializeField]
    private GameObject[] stars;

    public TimeLimit timeLimitData;
    private float usedTime;

    private PhotonView PV;

    private int starCount;
    

    // Start is called before the first frame update
    void Start()
    {
        timeLimitData = GameObject.Find("TimeLimit").GetComponent<TimeLimit>();
        PV = gameObject.GetComponentInParent<PhotonView>();
    }

    public void UpdateFinishedCard()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            usedTime = Time.timeSinceLevelLoad;
            Debug.Log("Send Used Time");
            SendUsedTime();
        }

        for (int i = 0; i < 4; i++)
        {
            //Debug.Log(timeLimitData);
            if (usedTime <= timeLimitData.timeLimits[i])
            {
                commentLabel.text = timeLimitData.comments[i];

                starCount = i;
                //StartCoroutine("ActivateStars", i);

                //for (int j = 2; j > i - 1; j--)
                //{
                //    stars[j].SetActive(true);
                //}

                break;
            }
            else
            {
                commentLabel.text = timeLimitData.comments[3];
            }
        }

        if (usedTime < 60)
        {
            timeLabel.text = "Ihr habt das Ziel in " + (int)usedTime + " Sekunden erreicht.";
        }
        else
        {
            timeLabel.text = "Ihr habt das Ziel in " + ConvertSecondsToMinutes(usedTime) + " Minuten erreicht.";
        }

        //levelauswahlPanel.GetComponent<CanvasGroup>().alpha = 1;
        //levelauswahlPanel.GetComponent<CanvasGroup>().interactable = true;
        //levelauswahlPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //levelButtons.SetActive(false);
        //nextLevelButton.SetActive(true);
    }

    public void ShowFinishedCard()
    {
        StartCoroutine("ActivateStars", starCount);
        levelauswahlPanel.GetComponent<CanvasGroup>().alpha = 1;
        levelauswahlPanel.GetComponent<CanvasGroup>().interactable = true;
        levelauswahlPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        levelButtons.SetActive(false);
        nextLevelButton.SetActive(true);
    }

    IEnumerator ActivateStars(int i)
    {
        for (int j = 2; j > i - 1; j--)
        {
            yield return new WaitForSeconds(0.3f);
            stars[j].SetActive(true);
            stars[j].GetComponent<Animator>().SetTrigger("StartAnimation");
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


    private void SendUsedTime()
    {
        Debug.Log(usedTime);
        PV.RPC("SendUsedTimeForOther", RpcTarget.OthersBuffered, gameObject.name, usedTime);
    }

    public void SetUsedTime(float usedTimeOther)
    {
        Debug.Log(usedTimeOther);
        usedTime = usedTimeOther;
        UpdateFinishedCard();
    }
}
