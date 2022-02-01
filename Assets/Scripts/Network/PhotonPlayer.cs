using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;

    private GameObject myAvatar;
    [SerializeField]
    private GameObject avatarCamera;

    private Transform fireSpawnPosition;
    private Transform waterSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        fireSpawnPosition = GameObject.Find("FeuerspielerSpawnPoint").transform;
        waterSpawnPosition = GameObject.Find("WasserspielerSpawnPoint").transform;

        PV = GetComponent<PhotonView>();


        if (PV.IsMine && PhotonNetwork.IsMasterClient)
        {
            Invoke("SpawnPlayerFire", 0.1f);
        }
        else if(PV.IsMine && !PhotonNetwork.IsMasterClient)
        {
            Invoke("SpawnPlayerWater", 0.1f);
        }
    }

    private void SpawnPlayerFire()
    {
        myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FeuerSpieler"), fireSpawnPosition.position, fireSpawnPosition.rotation);
        myAvatar.GetComponent<PlayerController>().inputController.gameObject.SetActive(true);
        myAvatar.GetComponent<PlayerController>().canvas.gameObject.SetActive(true);
        ConnectCameraToAvatar();
    }

    private void SpawnPlayerWater()
    {
        myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "WasserSpieler"), waterSpawnPosition.position, waterSpawnPosition.rotation);
        myAvatar.GetComponent<PlayerController>().inputController.gameObject.SetActive(true);
        myAvatar.GetComponent<PlayerController>().canvas.gameObject.SetActive(true);
        ConnectCameraToAvatar();
    }

    private void ConnectCameraToAvatar()
    {
        avatarCamera.GetComponent<CameraFollow>().Target = myAvatar.transform;
        avatarCamera.GetComponent<Camera>().gameObject.SetActive(true);
    }
}