using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private bool mobileInput = true;

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
        
        if (Input.touchCount != 0 || Input.GetMouseButton(0))
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
        joystick.gameObject.SetActive(true);
        supressButton.gameObject.SetActive(true);

        mobileInput = true;
    }

    private void ActivateKeyboardInput()
    {
        joystick.gameObject.SetActive(false);
        supressButton.gameObject.SetActive(false);

        mobileInput = false;
    }
}
