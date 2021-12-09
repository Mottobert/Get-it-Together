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

    private List<GameObject> playersOnPlatform = new List<GameObject>();

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
        Vector3 startPos;
        Vector3 endPos;

        startPos = platform.transform.position;
        platform.transform.position = Vector3.Lerp(platform.transform.position, toPoint.position, 0.01f);
        endPos = platform.transform.position;

        Vector3 movement = endPos - startPos;

        MoveAllPlayersOnPlatform(movement);
    }


    public void AddElementToPlayerList(GameObject player)
    {
        playersOnPlatform.Add(player);
    }

    public void RemoveElementFromPlayerList(GameObject player)
    {
        playersOnPlatform.Remove(player);
    }

    private void MoveAllPlayersOnPlatform(Vector3 movement)
    {
        if (playersOnPlatform.Count != 0)
        {
            //Debug.Log(playersOnPlatform[0]);
            foreach (GameObject p in playersOnPlatform)
            {
                Debug.Log(movement);
                p.GetComponent<CharacterController>().Move(movement);
            }
        }
    }
}
