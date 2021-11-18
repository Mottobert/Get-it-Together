using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public bool player1;

    private CharacterController controller;
    private Vector3 direction;
    public float speed = 8;

    public float jumpForce = 10;
    public float gravity = -20;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        float hInput = 0f;
        float vInput = 0f;

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        
        if (player1)
        {
            //hInput = movementInput.x;
            hInput = Input.GetAxis("Horizontal");
            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                //direction.y = jumpForce;
            }

            bool hitInfo = Physics.Raycast(transform.position, Vector3.up, 2f, ladderLayer);

            if (hitInfo)
            {
                vInput = Input.GetAxis("Vertical");
                //rb.velocity = new Vector3(rb.velocity.x, vInput, 0);

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

        } else if(!player1)
        {
            hInput = Input.GetAxis("Debug Horizontal");
            if (Input.GetButtonDown("Jump2") && isGrounded)
            {
                //direction.y = jumpForce;
            }

            bool hitInfo = Physics.Raycast(transform.position, Vector3.up, 2f, ladderLayer);
            //Debug.Log(hitInfo);

            if (hitInfo)
            {
                //Debug.Log("Ladder");
                vInput = Input.GetAxis("Debug Vertical");

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
        }

        direction.x = hInput * speed;
        

        
        //rb.velocity = new Vector3(rb.velocity.x, vInput * speed, rb.velocity.z);


        controller.Move(direction * Time.deltaTime);
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
}
