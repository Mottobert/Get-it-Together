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

    public bool active = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "water" || other.tag == "fire" || other.tag == "iceblock")
        {
            PhotonView PVPlayer = other.gameObject.GetComponentInParent<PhotonView>();

            //ActivatePressurePlate();
            PVPlayer.RPC("ActivatePressurePlateForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "water" || other.tag == "fire" || other.tag == "iceblock")
        {
            PhotonView PVPlayer = other.gameObject.GetComponentInParent<PhotonView>();

            //DeactivatePressurePlate();
            PVPlayer.RPC("DeactivatePressurePlateForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
    }

    public void ActivatePressurePlate()
    {
        plateObject.transform.position = new Vector3(plateObject.transform.position.x, plateObject.transform.position.y - 0.01f, plateObject.transform.position.z);
        active = true;
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
        plateObject.transform.position = new Vector3(plateObject.transform.position.x, plateObject.transform.position.y + 0.01f, plateObject.transform.position.z);
        active = false;
        if (movingPlatform)
        {
            movingPlatform.GetComponent<MovingPlatform>().DeactivateMovingPlatform();
        }

        if (puzzleManager)
        {
            puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
        }
    }
}
