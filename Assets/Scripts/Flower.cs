using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    private GameObject flowerObject;
    [SerializeField]
    private GameObject[] ladderObjects;

    private void Awake()
    {
        //gameObject.name = GetInstanceID().ToString();
    }

    private void ChangeObjectStatus(GameObject[] objects, bool status)
    {
        foreach(GameObject g in objects)
        {
            g.SetActive(status);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView PVPlayer = other.gameObject.GetComponent<PhotonView>();

        if(other.tag == "water" && PVPlayer.IsMine)
        {
            //ActivateLadder();
            PVPlayer.RPC("ActivateFlowerForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
        else if(other.tag == "fire" && PVPlayer.IsMine)
        {
            //DeactivateLadder();
            PVPlayer.RPC("DeactivateFlowerForAll", RpcTarget.AllBufferedViaServer, gameObject.name);
        }
    }

    public void ActivateLadder()
    {
        AnimateBlume(ladderObjects, true);
    }

    public void DeactivateLadder()
    {
        AnimateBlume(ladderObjects, false);
    }

    private void AnimateBlume(GameObject[] objects, bool status)
    {
        if (status)
        {
            StartCoroutine("ActivateBlume", objects);
        } 
        else if (!status)
        {
            StartCoroutine("DeactivateBlume", objects);
        }
    }

    IEnumerator ActivateBlume(GameObject[] objects)
    {
        flowerObject.GetComponent<Animator>().SetBool("active", false);
        yield return new WaitForSeconds(1f);
        //flowerObject.SetActive(false);

        foreach (GameObject g in objects)
        {
            g.SetActive(true);
            g.GetComponentInChildren<Animator>().SetBool("active", true);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator DeactivateBlume(GameObject[] objects)
    {
        for(int i = objects.Length - 1; i > -1; i--)
        {
            objects[i].GetComponentInChildren<Animator>().SetBool("active", false);
            yield return new WaitForSeconds(1.5f);
            objects[i].SetActive(false);
        }

        //flowerObject.SetActive(true);
        flowerObject.GetComponent<Animator>().SetBool("active", true);
        //yield return new WaitForSeconds(1f);
    }
}
