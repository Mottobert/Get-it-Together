using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //public bool player1;

    [SerializeField]
    private string verticalInput;
    [SerializeField]
    private string horizontalInput;
    [SerializeField]
    private string playerTag;
    [SerializeField]
    private string supressInput;


    private CharacterController controller;
    private Vector3 direction;
    public float speed = 8;

    public float jumpForce = 10;
    public float gravity = -20;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Vector2 movementInput = Vector2.zero;
    //private bool jumped = false;

    [SerializeField]
    private LayerMask ladderLayer;
    private Rigidbody rb;
    private bool gravityEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float hInput = 0f;
        float vInput = 0f;

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        //if(Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    direction.y = jumpForce;
        //}

        hInput = Input.GetAxis(horizontalInput);
        bool hitInfo = Physics.Raycast(transform.position, Vector3.up, 2f, ladderLayer);

        if (hitInfo)
        {
            vInput = Input.GetAxis(verticalInput);

            gravityEnabled = false;
        }
        else if(!hitInfo)
        {
            gravityEnabled = true;
        }

        if (gravityEnabled && !hitInfo)
        {
            direction.y += gravity * Time.deltaTime;
        }
        else
        {
            direction.y = vInput * speed / 2;
        }

        direction.x = hInput * speed;
        controller.Move(direction * Time.deltaTime);

        if (Input.GetKey(supressInput))
        {
            ChangeTag("Untagged");
        }
        else
        {
            ChangeTag(playerTag);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log("MoveTest");
        //movementInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        //Debug.Log("JumpTest");
        //jumped = context.action.triggered;
    }

    private void ChangeTag(string newTag)
    {
        gameObject.tag = newTag;
    }
}
