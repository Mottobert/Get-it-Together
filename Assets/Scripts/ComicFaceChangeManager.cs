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

    [SerializeField]
    private ComicFaceChangeController faceChangeController;

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
                otherPlayer = p;
            }
            else if (p.layer == LayerMask.NameToLayer("PlayerWater") && player != water)
            {
                otherPlayer = p;
            }
        }
    }

    public void CheckOtherPlayerPosition()
    {
        if(otherPlayer == null)
        {
            GetOtherPlayer();
        }

        if(otherPlayer && otherPlayer.transform.position.x >= currentPlayer.transform.position.x)
        {
            faceChangeController.StartRightLooking();
            Debug.Log("Other Player is right");
        }
        else if(otherPlayer && otherPlayer.transform.position.x < currentPlayer.transform.position.x)
        {
            faceChangeController.StartLeftLooking();
            Debug.Log("Other Player is left");
        }
    }
}
