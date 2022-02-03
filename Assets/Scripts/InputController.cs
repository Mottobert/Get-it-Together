using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public bool supressInput;

    [SerializeField]
    private FixedJoystick joystick;
    [SerializeField]
    private Button supressButton;

    [SerializeField]
    private GameObject mobileInputPanel;
    [SerializeField]
    private GameObject levelauswahlPanel;
    [SerializeField]
    private GameObject kommunikationPanel;
    [SerializeField]
    private GameObject menuePanel;
    [SerializeField]
    public GameObject finishedPanel;
    [SerializeField]
    private GameObject soundPanel;

    [SerializeField]
    private TextMeshProUGUI finishedPanelTimeLabel;

    private bool mobileInput = true;

    private string activeSceneName;

    [SerializeField]
    private GameObject kommunikationsButton;

    [SerializeField]
    private bool playSound = true;

    private GameObject uiAudioManager;

    private void Start()
    {
        uiAudioManager = GameObject.FindGameObjectWithTag("music");
        activeSceneName = SceneManager.GetActiveScene().name;

        playSound = false;

        if (activeSceneName != "Levelauswahl")
        {
            DeactivateLevelauswahlPanel();
            DeactivateKommunikationPanel();
            DeactivateMenuePanel();
            DeactivateFinishedPanel();
            DeactivateSoundPanel();
            kommunikationsButton.SetActive(true);
        }
        else
        {
            ActivateKeyboardInput();
            DeactivateKommunikationPanel();
            DeactivateMenuePanel();
            DeactivateFinishedPanel();
            DeactivateSoundPanel();
            ActivateLevelauswahlPanel();
            kommunikationsButton.SetActive(false);
        }

        playSound = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            if(!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
            {
                ActivateKeyboardInput();
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ToogleKommunikationPanel();
        }
        
        if ((Input.touchCount != 0 || Input.GetMouseButton(0)) && activeSceneName != "Levelauswahl")
        {
            ActivateMobileInput();
        }

        if (mobileInput)
        {
            horizontalInput = joystick.Horizontal;
            verticalInput = joystick.Vertical;
        }
        else if(!mobileInput)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            
            supressInput = Input.GetKey(KeyCode.Space);
        }
    }

    public void ButtonPressed()
    {
        supressInput = true;
    }

    public void ButtonReleased()
    {
        supressInput = false;
    }

    private void ActivateMobileInput()
    {
        //joystick.gameObject.SetActive(true);
        //supressButton.gameObject.SetActive(true);

        mobileInputPanel.SetActive(true);

        mobileInput = true;
    }

    private void ActivateKeyboardInput()
    {
        //joystick.gameObject.SetActive(false);
        //supressButton.gameObject.SetActive(false);

        mobileInputPanel.SetActive(false);

        mobileInput = false;
    }

    public void ActivateLevelauswahlPanel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        //levelauswahlPanel.SetActive(true);
        levelauswahlPanel.GetComponent<CanvasGroup>().alpha = 1;
        levelauswahlPanel.GetComponent<CanvasGroup>().interactable = true;
        levelauswahlPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        DeactivateKommunikationPanel();
        DeactivateMenuePanel();
        //mobileInputPanel.SetActive(false);
    }

    public void DeactivateLevelauswahlPanel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        //levelauswahlPanel.SetActive(false);
        //levelauswahlPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, -450);
        //levelauswahlPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, 450);

        levelauswahlPanel.GetComponent<CanvasGroup>().alpha = 0;
        levelauswahlPanel.GetComponent<CanvasGroup>().interactable = false;
        levelauswahlPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //mobileInputPanel.SetActive(true);
    }

    public void ToogleLevelauswahlPanel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        if (levelauswahlPanel.GetComponent<CanvasGroup>().interactable)
        {
            DeactivateLevelauswahlPanel();
        }
        else
        {
            ActivateLevelauswahlPanel();
        }
    }

    public void ToogleKommunikationPanel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        if (kommunikationPanel.GetComponent<CanvasGroup>().interactable)
        {
            DeactivateKommunikationPanel();
        }
        else
        {
            ActivateKommunikationPanel();
        }
    }

    public void ToogleMenuePanel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        if (menuePanel.GetComponent<CanvasGroup>().interactable)
        {
            DeactivateMenuePanel();
        }
        else
        {
            ActivateMenuePanel();
        }
    }

    public void ToogleFinishedPanel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        if (finishedPanel.GetComponent<CanvasGroup>().interactable)
        {
            DeactivateFinishedPanel();
        }
        else
        {
            ActivateFinishedPanel();
        }
    }

    public void ActivateKommunikationPanel()
    {
        //kommunikationPanel.SetActive(true);
        kommunikationPanel.GetComponent<CanvasGroup>().alpha = 1;
        kommunikationPanel.GetComponent<CanvasGroup>().interactable = true;
        kommunikationPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        DeactivateLevelauswahlPanel();
        DeactivateMenuePanel();
        DeactivateFinishedPanel();
    }

    public void DeactivateKommunikationPanel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        //kommunikationPanel.SetActive(false);
        kommunikationPanel.GetComponent<CanvasGroup>().alpha = 0;
        kommunikationPanel.GetComponent<CanvasGroup>().interactable = false;
        kommunikationPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Menu Panel
    public void ActivateMenuePanel()
    {
        //kommunikationPanel.SetActive(true);
        menuePanel.GetComponent<CanvasGroup>().alpha = 1;
        menuePanel.GetComponent<CanvasGroup>().interactable = true;
        menuePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        DeactivateLevelauswahlPanel();
        DeactivateKommunikationPanel();
        DeactivateFinishedPanel();
    }

    public void DeactivateMenuePanel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        //kommunikationPanel.SetActive(false);
        menuePanel.GetComponent<CanvasGroup>().alpha = 0;
        menuePanel.GetComponent<CanvasGroup>().interactable = false;
        menuePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Finished Panel
    public void ActivateFinishedPanel()
    {
        finishedPanel.GetComponent<CanvasGroup>().alpha = 1;
        finishedPanel.GetComponent<CanvasGroup>().interactable = true;
        finishedPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        DeactivateKommunikationPanel();
        DeactivateMenuePanel();
    }

    public void DeactivateFinishedPanel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        finishedPanel.GetComponent<CanvasGroup>().alpha = 0;
        finishedPanel.GetComponent<CanvasGroup>().interactable = false;
        finishedPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Sound Panel
    public void ActivateSoundPanel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        soundPanel.GetComponent<CanvasGroup>().alpha = 1;
        soundPanel.GetComponent<CanvasGroup>().interactable = true;
        soundPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        DeactivateLevelauswahlPanel();
        DeactivateMenuePanel();
        DeactivateKommunikationPanel();
    }

    public void DeactivateSoundPanel()
    {
        if (playSound)
        {
            uiAudioManager.GetComponent<MMFeedbacks>().PlayFeedbacks();
        }
        soundPanel.GetComponent<CanvasGroup>().alpha = 0;
        soundPanel.GetComponent<CanvasGroup>().interactable = false;
        soundPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //ActivateMenuePanel();
    }
}
