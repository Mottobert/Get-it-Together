using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private GameObject movingPlatform;

    [SerializeField]
    private GameObject plateObject;

    [SerializeField]
    private GameObject puzzleManager;

    [SerializeField]
    private GameObject indicatorLamp;
    [SerializeField]
    private Material lampActiveMat;
    [SerializeField]
    private Material lampInactiveMat;

    public bool active = false;

    public int collisionCounter;

    private Transform resetPosition;

    private PhotonView PVPlayer;

    private void Start()
    {
        resetPosition = this.gameObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "platformMovable")
        {
            PVPlayer = other.gameObject.GetComponentInParent<PhotonView>();

            //ActivatePressurePlate();
            PVPlayer.RPC("ActivatePressurePlateForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "platformMovable")
        {
            PVPlayer = other.gameObject.GetComponentInParent<PhotonView>();

            //DeactivatePressurePlate();
            PVPlayer.RPC("CheckCollisionCounterForMaster", RpcTarget.MasterClient, gameObject.name);
        }
    }

    public void ActivatePressurePlate()
    {
        plateObject.transform.position = new Vector3(plateObject.transform.position.x, resetPosition.position.y - 0.05f, plateObject.transform.position.z);
        active = true;

        if (PhotonNetwork.IsMasterClient)
        {
            collisionCounter++;
        }

        indicatorLamp.GetComponent<MeshRenderer>().material = lampActiveMat;

        if (movingPlatform)
        {
            movingPlatform.GetComponent<MovingPlatform>().ActivateMovingPlatform();
        }

        if (puzzleManager)
        {
            puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
        }
    }

    public void DeactivatePressurePlate()
    {
        plateObject.transform.position = resetPosition.position;
        active = false;

        if (PhotonNetwork.IsMasterClient)
        {
            collisionCounter--;
        }

        indicatorLamp.GetComponent<MeshRenderer>().material = lampInactiveMat;

        if (movingPlatform)
        {
            movingPlatform.GetComponent<MovingPlatform>().DeactivateMovingPlatform();
        }

        if (puzzleManager)
        {
            puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
        }
    }

    public void CheckCollisionCounter()
    {
        if (collisionCounter < 3)
        {
            PVPlayer.RPC("DeactivatePressurePlateForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        } 
        else
        {
            collisionCounter--;
        }
    }
}
