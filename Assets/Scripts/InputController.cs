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
    private GameObject finishedPanel;

    [SerializeField]
    private TextMeshProUGUI finishedPanelTimeLabel;

    private bool mobileInput = true;

    private string activeSceneName;

    [SerializeField]
    private GameObject kommunikationsButton;

    private void Start()
    {
        activeSceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName != "Levelauswahl")
        {
            DeactivateLevelauswahlPanel();
            DeactivateKommunikationPanel();
            DeactivateMenuePanel();
            DeactivateFinishedPanel();
            kommunikationsButton.SetActive(true);
        }
        else
        {
            ActivateKeyboardInput();
            DeactivateKommunikationPanel();
            DeactivateMenuePanel();
            DeactivateFinishedPanel();
            ActivateLevelauswahlPanel();
            kommunikationsButton.SetActive(false);
        }
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
        if (menuePanel.GetComponent<CanvasGroup>().interactable)
        {
            DeactivateMenuePanel();
        }
        else
        {
            ActivateMenuePanel();
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
    }

    public void DeactivateKommunikationPanel()
    {
        //kommunikationPanel.SetActive(false);
        kommunikationPanel.GetComponent<CanvasGroup>().alpha = 0;
        kommunikationPanel.GetComponent<CanvasGroup>().interactable = false;
        kommunikationPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void ActivateMenuePanel()
    {
        //kommunikationPanel.SetActive(true);
        menuePanel.GetComponent<CanvasGroup>().alpha = 1;
        menuePanel.GetComponent<CanvasGroup>().interactable = true;
        menuePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        DeactivateLevelauswahlPanel();
        DeactivateKommunikationPanel();
    }

    public void DeactivateMenuePanel()
    {
        //kommunikationPanel.SetActive(false);
        menuePanel.GetComponent<CanvasGroup>().alpha = 0;
        menuePanel.GetComponent<CanvasGroup>().interactable = false;
        menuePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    public void ActivateFinishedPanel()
    {
        finishedPanel.GetComponent<CanvasGroup>().alpha = 1;
        finishedPanel.GetComponent<CanvasGroup>().interactable = true;
        finishedPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        DeactivateKommunikationPanel();
        DeactivateMenuePanel();

        finishedPanelTimeLabel.text = "Ihr habt das Ziel in " + ConvertSecondsToMinutes(Time.timeSinceLevelLoad) + " Minuten erreicht.";
    }

    public void DeactivateFinishedPanel()
    {
        finishedPanel.GetComponent<CanvasGroup>().alpha = 0;
        finishedPanel.GetComponent<CanvasGroup>().interactable = false;
        finishedPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
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
