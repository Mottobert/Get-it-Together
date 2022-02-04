using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayObstacle : MonoBehaviour
{
    [SerializeField]
    private bool rightSideOpen;

    private BoxCollider obstacleCollider;

    public GameObject emojiAnimation;

    public ParticleSystem poofParticleSystem;

    public GameObject mesh;

    private void DisableCollider(string tag)
    {
        if(tag == "fire")
        {
            gameObject.layer = LayerMask.NameToLayer("ObstacleReleaseFire");
        }
        else if(tag == "water")
        {
            gameObject.layer = LayerMask.NameToLayer("ObstacleReleaseWater");
        }
    }

    private void EnableCollider()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void CheckRelease(bool right, string tag)
    {
        if (rightSideOpen && right)
        {
            DisableCollider(tag);
        }
        else if(rightSideOpen && !right)
        {
            EnableCollider();
        }
        else if (!rightSideOpen && !right)
        {
            DisableCollider(tag);
        }
        else if (!rightSideOpen && right)
        {
            EnableCollider();
        }
    }
}
