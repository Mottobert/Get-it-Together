using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] puzzleObjects;
    [SerializeField]
    private bool[] puzzleObjectsStatus; // Welchen Zustand sollen die Objekte haben, damit das Puzzle gel?st ist
    [SerializeField]
    private GameObject solvedObject; // Objekt welches aktiviert werden soll, wenn das Puzzle gel?st wurde

    private GameObject finish;

    public bool solved = false;

    private void Start()
    {
        finish = GameObject.FindGameObjectWithTag("finish");

        foreach(GameObject p in puzzleObjects)
        {
            if (p.GetComponent<Fackel>())
            {
                p.GetComponent<Fackel>().puzzleManager = this.gameObject;
            }
            else if (p.GetComponent<Waterfall>())
            {
                p.GetComponent<Waterfall>().puzzleManager = this.gameObject;
            }
        }

        CheckPuzzleObjects();
    }

    public void CheckPuzzleObjects()
    {
        for(int i = 0; i < puzzleObjects.Length; i++)
        {
            if(puzzleObjects[i].GetComponent<Fackel>() && !(puzzleObjects[i].GetComponent<Fackel>().activeFlame == puzzleObjectsStatus[i]))
            {
                PuzzleUnsolved();
                return;
            }
            else if(puzzleObjects[i].GetComponent<Waterfall>() && !(puzzleObjects[i].GetComponent<Waterfall>().activeWaterfall == puzzleObjectsStatus[i]))
            {
                PuzzleUnsolved();
                return;
            }
            else if (puzzleObjects[i].GetComponent<PressurePlate>() && !(puzzleObjects[i].GetComponent<PressurePlate>().active == puzzleObjectsStatus[i]))
            {
                PuzzleUnsolved();
                return;
            }
        }

        PuzzleSloved();
    }

    private void PuzzleSloved()
    {
        //Debug.Log("Puzzle Solved");
        if (solvedObject)
        {
            solvedObject.GetComponent<OneWayObstacle>().mesh.SetActive(false);
            solvedObject.GetComponent<OneWayObstacle>().emojiAnimation.SetActive(true);
            solvedObject.GetComponent<OneWayObstacle>().emojiAnimation.GetComponent<Animator>().SetBool("activate", true);
            solvedObject.GetComponent<OneWayObstacle>().poofParticleSystem.Play();

            solvedObject.GetComponent<OneWayObstacle>().gameObject.GetComponent<BoxCollider>().enabled = false;
            solved = true;

            if (finish)
            {
                finish.GetComponent<Finish>().CheckFinishRequirements();
            }
        }
    }

    private void PuzzleUnsolved()
    {
        //Debug.Log("Puzzle Unsolved");
        if (solvedObject)
        {
            solvedObject.GetComponent<OneWayObstacle>().mesh.SetActive(true);
            solvedObject.GetComponent<OneWayObstacle>().emojiAnimation.GetComponent<Animator>().SetBool("activate", false);

            solvedObject.GetComponent<OneWayObstacle>().gameObject.GetComponent<BoxCollider>().enabled = true;
            solved = false;

            if (finish)
            {
                finish.GetComponent<Finish>().CheckFinishRequirements();
            }
        }
    }
}
