using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowStarInfoText : MonoBehaviour
{
    [SerializeField]
    private int buttonIndex;
    [SerializeField]
    private FinishedController finishedController;

    [SerializeField]
    private TextMeshProUGUI infoTextLabel;
    [SerializeField]
    private Animator infoTextAnimator;

    [SerializeField]
    private bool playSound = true;

    private GameObject uiAudioManager;

    private void Start()
    {
        uiAudioManager = GameObject.FindGameObjectWithTag("music");
    }

    public void ShowInfoText()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        StopCoroutine("HideInfoText");
        infoTextLabel.text = "Ihr müsst das Ziel in unter " + finishedController.timeLimitData.timeLimits[buttonIndex] + " Sekunden erreichen um diesen Stern zu erhalten";
        infoTextAnimator.SetBool("ShowInfoText", true);
        StartCoroutine("HideInfoText");
    }

    IEnumerator HideInfoText()
    {
        yield return new WaitForSeconds(5f);
        infoTextAnimator.SetBool("ShowInfoText", false);
    }
}
