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
    private GameObject waterfallPlane;

    [SerializeField]
    private ParticleSystem dripParticleSystem;
    [SerializeField]
    private ParticleSystem flowParticleSystem;

    [SerializeField]
    private AudioClip dripAudioClip;
    [SerializeField]
    private AudioClip flowAudioClip;

    [SerializeField]
    private bool spawnIceblock;

    public bool activeWaterfall = false;

    public GameObject puzzleManager;

    [SerializeField]
    private HintManager hintManager;

    private GameObject finish;

    [SerializeField]
    private float deactivateDelay;

    private void Awake()
    {
        finish = GameObject.FindGameObjectWithTag("finish");
        //gameObject.name = GetInstanceID().ToString(); // Sollte aktiviert werden, wenn das Spiel final gebuilded wird
        dripParticleSystem.Play();
        flowParticleSystem.Pause();

        if (dripAudioClip)
        {
            this.gameObject.GetComponent<AudioSource>().clip = dripAudioClip;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView PVPlayer = other.gameObject.GetComponent<PhotonView>();

        if (other.tag == "water" && !activeWaterfall && PVPlayer.IsMine)
        {
            //ActivateWaterfall();
            PVPlayer.RPC("ActivateWaterfallForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
        else if(other.tag == "fire" && PVPlayer.IsMine) {
            //DeactivateWaterfall();
            PVPlayer.RPC("DeactivateWaterfallForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
        if (other.tag == "water" && !activeIceblock && spawnIceblock && PVPlayer.IsMine)
        {
            activeIceblock = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Eisblock"), iceblockSpawnPoint.position, Quaternion.identity); //Instantiate(iceblockObject, iceblockSpawnPoint.position, Quaternion.identity);
        }
    }

    public void ActivateWaterfall()
    {
        waterfallObject.GetComponent<MeshRenderer>().material = waterfallFlowingMaterial;
        waterfallPlane.SetActive(true);

        dripParticleSystem.Stop();
        flowParticleSystem.Play();

        activeWaterfall = true;

        if (flowAudioClip)
        {
            this.gameObject.GetComponent<AudioSource>().clip = flowAudioClip;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
        }

        if (puzzleManager)
        {
            puzzleManager.GetComponent<Puzzle>().CheckPuzzleObjects();
        }

        if (hintManager)
        {
            hintManager.ActivateHintObjects();
        }

        if (finish)
        {
            finish.GetComponent<Finish>().CheckFinishRequirements();
        }

        if (deactivateDelay != 0)
        {
            Invoke("DeactivateWaterfall", deactivateDelay);
        }
    }

    public void DeactivateWaterfall()
    {
        waterfallObject.GetComponent<MeshRenderer>().material = waterfallEmptyMaterial;
        waterfallPlane.SetActive(false);

        dripParticleSystem.Play();
        flowParticleSystem.Stop();

        activeWaterfall = false;

        if (dripAudioClip)
        {
            this.gameObject.GetComponent<AudioSource>().clip = dripAudioClip;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
        }

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
