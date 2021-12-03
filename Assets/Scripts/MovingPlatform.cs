using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform endPoint;
    [SerializeField]
    private GameObject platform;

    [SerializeField]
    private float movingSpeed;

    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        platform.transform.position = startPoint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            MovePlatform(endPoint);
        }
        else if (!active)
        {
            MovePlatform(startPoint);
        }
    }

    public void ActivateMovingPlatform()
    {
        active = true;
    }

    public void DeactivateMovingPlatform()
    {
        active = false;
    }

    private void MovePlatform(Transform toPoint)
    {
        platform.transform.position = Vector3.Lerp(platform.transform.position, toPoint.position, 0.01f);
    }
}
