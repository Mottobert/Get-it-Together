using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class OffscreenCanvas : MonoBehaviour
{
    [SerializeField]
    private PhotonView PV;
    [SerializeField]
    private Transform resetPosition;

    public Camera mainCamera;
    public GameObject targetPlayer;
    public GameObject currentPlayer;

    public Vector3 indicatorPosition;


    // Update is called once per frame
    void Update()
    {
        if(mainCamera == null)
        {
            mainCamera = GetActiveCamera();
        }

        if (targetPlayer == null)
        {
            targetPlayer = GetOtherPlayer();
        }

        if (currentPlayer == null)
        {
            currentPlayer = GetCurrentPlayer();
        }

        if (mainCamera != null && targetPlayer != null)
        {
            indicatorPosition = mainCamera.WorldToScreenPoint(targetPlayer.transform.position);
            //Debug.Log(indicatorPosition);
        }

        //if (!PV.IsMine && mainCamera != null && targetPlayer != null)

        //  
        if (!PV.IsMine && mainCamera != null && targetPlayer != null && currentPlayer != null)
            {
            //Debug.Log(indicatorPosition);
            if(targetPlayer.transform.position.x < currentPlayer.transform.position.x && indicatorPosition.x < 100)
            {
                SetCanvasToLeftSide();
            }
            else if(targetPlayer.transform.position.x > currentPlayer.transform.position.x && indicatorPosition.x > Screen.width - 100)
            {
                SetCanvasToRightSide();
            }
            else
            {
                ResetCanvasPosition();
            }
        }
    }

    private GameObject GetOtherPlayer()
    {
        PlayerController[] players = GameObject.FindObjectsOfType<PlayerController>();

        //Debug.Log(players);

        foreach(PlayerController p in players)
        {
            if (!p.gameObject.GetComponent<PhotonView>().IsMine)
            {
                return p.gameObject;
            }

            //if (p.gameObject.tag == "water")
            //{
            //    return p.gameObject;
            //}
        }

        return null;
    }

    private GameObject GetCurrentPlayer()
    {
        PlayerController[] players = GameObject.FindObjectsOfType<PlayerController>();

        //Debug.Log(players);

        foreach (PlayerController p in players)
        {
            if (p.gameObject.GetComponent<PhotonView>().IsMine)
            {
                return p.gameObject;
            }

            //if (p.gameObject.tag == "fire")
            //{
            //    return p.gameObject;
            //}
        }

        return null;
    }

    private Camera GetActiveCamera()
    {
        Camera[] cameras = GameObject.FindObjectsOfType<Camera>();

        foreach(Camera c in cameras)
        {
            if (c.isActiveAndEnabled)
            {
                return c;
            }
        }
        return null;
    }

    private void SetCanvasToRightSide()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width-120, Screen.height/2, 8));
        //Debug.Log(newPosition);
        this.gameObject.transform.position = newPosition;
    }

    private void SetCanvasToLeftSide()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(new Vector3(80, Screen.height/2, 8));
        //Debug.Log(newPosition);
        this.gameObject.transform.position = newPosition;
    }

    private void ResetCanvasPosition()
    {
        this.gameObject.transform.position = resetPosition.position;
    }

    // if !Mine && Player.Offscreen
    // if Player.position < MyPlayer.position
    // position Canvas on the right side
    // else
    // position Canvas on the left side
}
