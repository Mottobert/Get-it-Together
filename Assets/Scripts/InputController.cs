using System.Collections;
using System.Collections.Generic;
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

    private bool mobileInput = true;

    private string activeSceneName;

    [SerializeField]
    private GameObject levelauswahlButton;

    private void Start()
    {
        activeSceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName != "Levelauswahl")
        {
            DeactivateLevelauswahlPanel();
            levelauswahlButton.SetActive(true);
        }
        else
        {
            ActivateKeyboardInput();
            levelauswahlButton.SetActive(false);
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
        //mobileInputPanel.SetActive(false);
    }

    public void DeactivateLevelauswahlPanel()
    {
        //levelauswahlPanel.SetActive(false);
        //levelauswahlPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, -450);
        //levelauswahlPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, 450);

        levelauswahlPanel.GetComponent<CanvasGroup>().alpha = 0;
        levelauswahlPanel.GetComponent<CanvasGroup>().interactable = false;
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
}
