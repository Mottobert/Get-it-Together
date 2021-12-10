using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayObstacle : MonoBehaviour
{
    [SerializeField]
    private bool rightSided;

    private BoxCollider obstacleCollider;

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
        if (rightSided && right)
        {
            DisableCollider(tag);
        }
        else if(rightSided && !right)
        {
            EnableCollider();
        }
        else if (!rightSided && !right)
        {
            DisableCollider(tag);
        }
        else if (!rightSided && right)
        {
            EnableCollider();
        }
    }
}
