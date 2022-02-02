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

    public bool active;

    private List<GameObject> playersOnPlatform = new List<GameObject>();

    [SerializeField]
    private GameObject activeEmoji;
    [SerializeField]
    private GameObject inactiveEmoji;

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
            if(Vector3.Distance(platform.transform.position, endPoint.position) > 0.1f)
            {
                MovePlatform(endPoint);
                activeEmoji.SetActive(true);
                inactiveEmoji.SetActive(false);
            }
            else
            {
                platform.transform.position = endPoint.position;
                activeEmoji.SetActive(false);
            }
            
        }
        else if (!active)
        {
            if (Vector3.Distance(platform.transform.position, startPoint.position) > 0.1f)
            {
                MovePlatform(startPoint);
                inactiveEmoji.SetActive(true);
                activeEmoji.SetActive(false);
            }
            else
            {
                platform.transform.position = startPoint.position;
                inactiveEmoji.SetActive(false);
            }
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
        platform.transform.position = Vector3.Lerp(platform.transform.position, toPoint.position, movingSpeed);
        endPos = platform.transform.position;

        Vector3 movement = endPos - startPos;

        //Debug.Log(startPos.x + " " + endPos.x + " " + movement.x);

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
            //Debug.Log(playersOnPlatform.Count);
            foreach (GameObject p in playersOnPlatform)
            {
                if (p.GetComponentInParent<CharacterController>())
                {
                    p.GetComponentInParent<CharacterController>().Move(movement);
                    //Debug.Log(movement);
                    //Debug.Log(p.transform.position.x);
                }
                else
                {
                    p.transform.parent.transform.position = p.transform.parent.transform.position + movement;
                }
            }
        }
    }
}
