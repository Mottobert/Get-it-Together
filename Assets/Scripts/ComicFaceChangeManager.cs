using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicFaceChangeManager : MonoBehaviour
{
    private GameObject otherPlayer;
    private GameObject currentPlayer;
    private int player;
    private int fire = 0;
    private int water = 1;

    private int lookingDirection;
    private int right = 0;
    private int left = 1;
    private int up = 2;
    private int down = 3;

    [SerializeField]
    private ComicFaceChangeController faceChangeController;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;

    private int lastLookingDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = CheckPlayerType();
        GetCurrentPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        CheckOtherPlayerPosition();
    }

    private void FixedUpdate()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        if (!isGrounded)
        {
            faceChangeController.StartDownLooking();
        } else
        {
            if(lookingDirection == right)
            {
                faceChangeController.StartRightLooking();
            }
            else if(lookingDirection == left)
            {
                faceChangeController.StartLeftLooking();
            }
        }
    }

    private void GetCurrentPlayer()
    {
        currentPlayer = this.gameObject.GetComponentInParent<PlayerController>().gameObject;
    }

    private int CheckPlayerType()
    {
        if(gameObject.tag == "fire")
        {
            return fire;
        }
        else if(gameObject.tag == "water")
        {
            return water;
        }
        return -1;
    }

    private void GetOtherPlayer()
    {
        GameObject[] players;

        players = GameObject.FindGameObjectsWithTag("playerFinishCollider");

        foreach(GameObject p in players)
        {
            if(p.layer == LayerMask.NameToLayer("PlayerFire") && player != fire)
            {
                otherPlayer = p.GetComponentInParent<PlayerController>().gameObject;
            }
            else if (p.layer == LayerMask.NameToLayer("PlayerWater") && player != water)
            {
                otherPlayer = p.GetComponentInParent<PlayerController>().gameObject;
            }
        }
    }

    public void CheckOtherPlayerPosition()
    {
        if(otherPlayer == null)
        {
            GetOtherPlayer();
        }

        if (otherPlayer && otherPlayer.transform.position.x >= currentPlayer.transform.position.x && lookingDirection != right)
        {
            faceChangeController.StartRightLooking();
            lookingDirection = right;
            Debug.Log("Other Player is right");
        }
        else if(otherPlayer && otherPlayer.transform.position.x < currentPlayer.transform.position.x && lookingDirection != left)
        {
            faceChangeController.StartLeftLooking();
            lookingDirection = left;
            Debug.Log("Other Player is left");
        }
    }
}
