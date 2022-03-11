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
    private int happy = 4;
    private int jump = 5;

    [SerializeField]
    private ComicFaceChangeController faceChangeController;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask ladderLayer;

    private bool otherPlayerTimerStarted = false;
    private bool checkOtherPlayer = false;

    private Vector3 previousPosition;
    private float verticalDifference;
    private float horizontalDifference;

    // Start is called before the first frame update
    void Start()
    {
        player = CheckPlayerType();
        GetCurrentPlayer();
    }

    private void FixedUpdate()
    {
        if (previousPosition != null)
        {
            verticalDifference = this.transform.position.x - previousPosition.x;
            horizontalDifference = this.transform.position.y - previousPosition.y;

            previousPosition = this.transform.position;
        }

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
        bool hitInfoLadder = Physics.Raycast(transform.position, Vector3.up, 2f, ladderLayer);

        if (!isGrounded && !hitInfoLadder)
        {
            faceChangeController.StartJumpLooking();
            lookingDirection = jump;
            StopCoroutine(LookAfterOtherPlayerTimer());
            otherPlayerTimerStarted = false;
            checkOtherPlayer = false;
        }
        else if (hitInfoLadder && horizontalDifference > 0)
        {
            faceChangeController.StartUpLooking();
            lookingDirection = up;
            StopCoroutine(LookAfterOtherPlayerTimer());
            otherPlayerTimerStarted = false;
            checkOtherPlayer = false;
        }
        else if (hitInfoLadder && horizontalDifference < 0)
        {
            faceChangeController.StartDownLooking();
            lookingDirection = down;
            StopCoroutine(LookAfterOtherPlayerTimer());
            otherPlayerTimerStarted = false;
            checkOtherPlayer = false;
        }
        else
        {
            if(verticalDifference > 0)
            {
                faceChangeController.StartRightLooking();
                lookingDirection = right;
                StopCoroutine(LookAfterOtherPlayerTimer());
                otherPlayerTimerStarted = false;
                checkOtherPlayer = false;
            }
            else if(verticalDifference < 0)
            {
                faceChangeController.StartLeftLooking();
                lookingDirection = left;
                StopCoroutine(LookAfterOtherPlayerTimer());
                otherPlayerTimerStarted = false;
                checkOtherPlayer = false;
            }
            else if(!otherPlayerTimerStarted)
            {
                faceChangeController.StartHappyLooking();
                lookingDirection = happy;
                StartCoroutine(LookAfterOtherPlayerTimer());
                otherPlayerTimerStarted = true;
            }

            if (checkOtherPlayer)
            {
                CheckOtherPlayerPosition();
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
            faceChangeController.StartHappyLooking();
            lookingDirection = happy;
        }

        if (otherPlayer && otherPlayer.transform.position.x >= currentPlayer.transform.position.x && lookingDirection != right)
        {
            faceChangeController.StartRightLooking();
            lookingDirection = right;
        }
        else if(otherPlayer && otherPlayer.transform.position.x < currentPlayer.transform.position.x && lookingDirection != left)
        {
            faceChangeController.StartLeftLooking();
            lookingDirection = left;
        }
    }

    IEnumerator LookAfterOtherPlayerTimer()
    {
        yield return new WaitForSeconds(2f);
        checkOtherPlayer = true;
    }
}
