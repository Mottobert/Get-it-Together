using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceblock : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;

    private Vector3 previousPosition;

    private Transform iceblockTransform;

    private void Start()
    {
        iceblockTransform = this.gameObject.transform;

        previousPosition = iceblockTransform.position;
    }

    private void Update()
    {
        iceblockTransform.rotation = Quaternion.identity;
        iceblockTransform.position = new Vector3(iceblockTransform.position.x, iceblockTransform.position.y, 0);

        //if(previousPosition != iceblockTransform.position)
        //{
        //    Debug.Log("Play Particles");
        //    particleSystem.Play();
        //    StopCoroutine(StopParticleSystem());
        //}
        //else
        //{
        //    if (!particleSystem.isStopped)
        //    {
        //        Debug.Log("Stop Particle");
        //        StartCoroutine(StopParticleSystem());
        //    }
        //}
        //
        //if(Time.frameCount % 20 == 0)
        //{
        //    previousPosition = iceblockTransform.position;
        //}
    }

    IEnumerator StopParticleSystem()
    {
        yield return new WaitForSeconds(0.3f);
        particleSystem.Stop();
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fire")
        {
            Destroy(this.gameObject);
        }
    }

    // Pressure Plate
    [PunRPC]
    public void ActivatePressurePlateForAll(string name)
    {
        //Debug.Log("Activate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<PressurePlate>().ActivatePressurePlate();
    }

    [PunRPC]
    public void DeactivatePressurePlateForAll(string name)
    {
        //Debug.Log("Deactivate Flower For All received");
        //Debug.Log(name);
        GameObject.Find(name).GetComponent<PressurePlate>().DeactivatePressurePlate();
    }
}
