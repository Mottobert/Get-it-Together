using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private float pushPower = 2.0f;

    [SerializeField]
    private string supressInput;

    [SerializeField]
    public InputController inputController;

    [SerializeField]
    private string playerTag;
    

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
    private ParticleSystem playerParticleSystem;

    [SerializeField]
    private GameObject pointLightFirePlayer;

    private bool abilityActive;

    [SerializeField]
    private PhotonView PV;

    public GameObject canvas;

    [SerializeField]
    private BoxCollider triggerCollider;

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

        if(inputController.verticalInput > 0.5 && isGrounded)
        {
            direction.y = jumpForce;
        }

        hInput = inputController.horizontalInput;  // Input.GetAxis(horizontalInput);
        bool hitInfoLadder = Physics.Raycast(transform.position, Vector3.up, 2f, ladderLayer);

        if (hitInfoLadder)
        {
            vInput = inputController.verticalInput; // Input.GetAxis(verticalInput);

            gravityEnabled = false;
        }
        else if(!hitInfoLadder)
        {
            gravityEnabled = true;
        }

        if (gravityEnabled && !hitInfoLadder)
        {
            direction.y += gravity * Time.deltaTime;
        }
        else
        {
            direction.y = vInput * speed / 2;
        }

        direction.x = hInput * speed;
        controller.Move(direction * Time.deltaTime);
        controller.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y, 0);

        if (inputController.supressInput && abilityActive && PV.IsMine)
        {
            //DeactivateAbility();
            PV.RPC("DeactivateAbilityForAll", RpcTarget.All, PV.ViewID);
        }
        else if(!inputController.supressInput && !abilityActive && PV.IsMine)
        {
            //ActivateAbility();
            PV.RPC("ActivateAbilityForAll", RpcTarget.All, PV.ViewID);
        }
    }


    private void ChangeTag(string newTag)
    {
        gameObject.tag = newTag;
    }

    private void ActivateAbility()
    {
        ChangeTag(playerTag);
        visualObject.GetComponent<MeshRenderer>().material = activeMaterial;
        playerParticleSystem.Play();
        triggerCollider.enabled = true;
        abilityActive = true;
        if (pointLightFirePlayer)
        {
            pointLightFirePlayer.SetActive(true);
        }
    }

    private void DeactivateAbility()
    {
        ChangeTag("Untagged");
        visualObject.GetComponent<MeshRenderer>().material = inactiveMaterial;
        playerParticleSystem.Stop();
        triggerCollider.enabled = false;
        abilityActive = false;
        if (pointLightFirePlayer)
        {
            pointLightFirePlayer.SetActive(false);
        }
    }

    [PunRPC]
    public void ActivateAbilityForAll(int viewID)
    {
        //Debug.Log("Activate Ability For All received");
        //Debug.Log(viewID);
        PhotonView.Find(viewID).GetComponent<PlayerController>().ActivateAbility();
    }

    [PunRPC]
    public void DeactivateAbilityForAll(int viewID)
    {
        //Debug.Log("Deactivate Ability For All received");
        //Debug.Log(viewID);
        PhotonView.Find(viewID).GetComponent<PlayerController>().DeactivateAbility();
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




    // Wood
    [PunRPC]
    public void ActivateWoodForAll(string name)
    {
        //Debug.Log("Activate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<Wood>().ActivateWood();
    }

    [PunRPC]
    public void DeactivateWoodForAll(string name)
    {
        //Debug.Log("Deactivate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<Wood>().DeactivateWood();
    }


    // Flower
    [PunRPC]
    public void ActivateFlowerForAll(string name)
    {
        //Debug.Log("Activate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<Flower>().ActivateLadder();
    }

    [PunRPC]
    public void DeactivateFlowerForAll(string name)
    {
        //Debug.Log("Deactivate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<Flower>().DeactivateLadder();
    }


    // Fackel
    [PunRPC]
    public void ActivateFackelForAll(string name)
    {
        //Debug.Log("Activate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<Fackel>().ActivateFlame();
    }

    [PunRPC]
    public void DeactivateFackelForAll(string name)
    {
        //Debug.Log("Deactivate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<Fackel>().DeactivateFlame();
    }


    // Flower
    [PunRPC]
    public void ActivateWaterfallForAll(string name)
    {
        //Debug.Log("Activate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<Waterfall>().ActivateWaterfall();
    }

    [PunRPC]
    public void DeactivateWaterfallForAll(string name)
    {
        //Debug.Log("Deactivate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<Waterfall>().DeactivateWaterfall();
    }


    // Pressure Plate
    [PunRPC]
    public void ActivatePressurePlateForAll(string name)
    {
        //Debug.Log("Activate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<PressurePlate>().ActivatePressurePlate();
    }

    [PunRPC]
    public void DeactivatePressurePlateForAll(string name)
    {
        //Debug.Log("Deactivate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<PressurePlate>().DeactivatePressurePlate();
    }


    // Vote Poll
    [PunRPC]
    public void VoteUpForAll(string name, bool masterClient)
    {
        //Debug.Log("Activate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<LevelSelectionButton>().VoteUp(masterClient);
    }

    [PunRPC]
    public void VoteDownForAll(string name, bool masterClient)
    {
        //Debug.Log("Activate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<LevelSelectionButton>().VoteDown(masterClient);
    }


    // Emoji
    [PunRPC]
    public void DisplayEmojiForAll(string name, int index)
    {
        //Debug.Log("Activate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<DisplayEmojiController>().DisplayEmojiForPlayers(index);
    }


    // Reload Level
    [PunRPC]
    public void ReloadSceneForAll(string name)
    {
        GameObject.Find(name).GetComponent<ChangeScene>().ReloadScene();
    }


    // Used Time
    [PunRPC]
    public void SendUsedTimeForOther(string name, float usedTime)
    {
        Debug.Log(usedTime);
        GameObject.Find(name).GetComponent<FinishedController>().SetUsedTime(usedTime);
    }


    // Finish
    [PunRPC]
    public void LevelFinishedForAll(string name)
    {
        //Debug.Log("Activate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<Finish>().LevelFinished();

        StartCoroutine("FinishedPanel");
    }

    IEnumerator FinishedPanel()
    {
        yield return new WaitForSeconds(1.8f);
        inputController.ActivateLevelauswahlPanel();
        inputController.ActivateFinishedPanel();
    }
}
