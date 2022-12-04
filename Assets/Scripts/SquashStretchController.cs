using Deform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashStretchController : MonoBehaviour
{
    [SerializeField] SquashAndStretchDeformer deformer;
    [SerializeField] float collisionRadius;

    [Header("Spring")]
    public float Strength = 15f;
    public float Dampening = 0.3f;
    public float VelocityStretch;

    private Vector3 _lastPosition;
    private Vector3 velocity;
    private float _squash;
    private float _squashVelocity;

    private void LateUpdate()
    {
        velocity = (transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = transform.position;

        ////If the sphere is position low enough to be colliding with the ground...
        //if (transform.position.y < collisionRadius)
        //{
        //    //Calculate how mush the sphere needs to be squashed to avoid intersecting with the ground.
        //    float targetSquash = (collisionRadius - transform.position.y) / collisionRadius;
        //
        //    //Store the squash velocity.
        //    _squashVelocity = targetSquash - _squash;
        //
        //    //Store the current squash value.
        //    _squash = targetSquash;
        //
        //    //_squash = -_squash;
        //}
        //else
        //{
            //Calculate the desired squash amount based on the current Y axis velocity.
            float targetSquash = -Mathf.Abs(velocity.y) * VelocityStretch;

            //Adjust the squash velocity.
            _squashVelocity += (targetSquash - _squash) * Strength * Time.deltaTime;

            //Apply dampening to the squash velocity.
            _squashVelocity = ((_squashVelocity / Time.deltaTime) * (1f - Dampening)) * Time.deltaTime;

            //Apply the velocity to the squash value.
            _squash += _squashVelocity;
        //}

        deformer.Factor = _squash;
    }
}
