using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private float pushPower = 2.0f;

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

    [SerializeField]
    private GameObject visualObject;
    [SerializeField]
    private Material activeMaterial;
    [SerializeField]
    private Material inactiveMaterial;

    [SerializeField]
    private GameObject pointLightFirePlayer;

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

        if(Input.GetAxis(verticalInput) > 0 && isGrounded)
        {
            direction.y = jumpForce;
        }

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
            DeactivateAbility();
        }
        else
        {
            ActivateAbility();
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

    private void ActivateAbility()
    {
        ChangeTag(playerTag);
        visualObject.GetComponent<MeshRenderer>().material = activeMaterial;
        if (pointLightFirePlayer)
        {
            pointLightFirePlayer.SetActive(true);
        }
    }

    private void DeactivateAbility()
    {
        ChangeTag("Untagged");
        visualObject.GetComponent<MeshRenderer>().material = inactiveMaterial;
        if (pointLightFirePlayer)
        {
            pointLightFirePlayer.SetActive(false);
        }
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
