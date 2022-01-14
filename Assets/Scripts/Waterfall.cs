using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    [SerializeField]
    private Material waterfallEmptyMaterial;
    [SerializeField]
    private Material waterfallFlowingMaterial;
    [SerializeField]
    private GameObject waterfallObject;
    [SerializeField]
    private Transform iceblockSpawnPoint;
    [SerializeField]
    private GameObject iceblockObject;
    private GameObject activeIceblock;

    [SerializeField]
    private ParticleSystem dripParticleSystem;
    [SerializeField]
    private ParticleSystem flowParticleSystem;

    [SerializeField]
    private bool spawnIceblock;

    public bool activeWaterfall = false;

    public GameObject puzzleManager;

    [SerializeField]
    private HintManager hintManager;

    private void Awake()
    {
        //gameObject.name = GetInstanceID().ToString(); // Sollte aktiviert werden, wenn das Spiel final gebuilded wird
        dripParticleSystem.Play();
        flowParticleSystem.Pause();
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView PVPlayer = other.gameObject.GetComponent<PhotonView>();

        if (other.tag == "water" && !activeWaterfall && PVPlayer.IsMine)
        {
            //ActivateWaterfall();
            PVPlayer.RPC("ActivateWaterfallForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
        else if (other.tag == "water" && activeWaterfall && !activeIceblock && spawnIceblock && PVPlayer.IsMine)
        {
            activeIceblock = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Eisblock"), iceblockSpawnPoint.position, Quaternion.identity); //Instantiate(iceblockObject, iceblockSpawnPoint.position, Quaternion.identity);
        }
        else if(other.tag == "fire" && PVPlayer.IsMine) {
            //DeactivateWaterfall();
            PVPlayer.RPC("DeactivateWaterfallForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    PhotonView PVPlayer = other.gameObject.GetComponent<PhotonView>();
    //
    //    if (movingPlatform && other.tag == "water" && PVPlayer.IsMine)
    //    {
    //        PVPlayer.RPC("DeactivateWaterfallForAll", RpcTarget.All, gameObject.name);
    //    }
    //}

    public void ActivateWaterfall()
    {
        waterfallObject.GetComponent<MeshRenderer>().material = waterfallFlowingMaterial;
        dripParticleSystem.Stop();
        flowParticleSystem.Play();

        activeWaterfall = true;
        if (puzzleManager)
        {
            puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
        }

        if (hintManager)
        {
            hintManager.ActivateHintObjects();
        }
    }

    public void DeactivateWaterfall()
    {
        waterfallObject.GetComponent<MeshRenderer>().material = waterfallEmptyMaterial;
        dripParticleSystem.Play();
        flowParticleSystem.Stop();

        activeWaterfall = false;
        if (puzzleManager)
        {
            puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
        }

        if (hintManager)
        {
            hintManager.DeactivateHintObjects();
        }
    }
}
