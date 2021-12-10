using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] puzzleObjects;
    [SerializeField]
    private bool[] puzzleObjectsStatus; // Welchen Zustand sollen die Objekte haben, damit das Puzzle gelöst ist
    [SerializeField]
    private GameObject solvedObject; // Objekt welches aktiviert werden soll, wenn das Puzzle gelöst wurde

    private void Start()
    {
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
        solvedObject.SetActive(false);
    }

    private void PuzzleUnsolved()
    {
        //Debug.Log("Puzzle Unsolved");
        solvedObject.SetActive(true);
    }
}
