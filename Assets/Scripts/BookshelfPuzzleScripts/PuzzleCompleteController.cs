using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleCompleteController : MonoBehaviour
{
    [System.NonSerialized] public bool puzzleCompleted;
    private int total;
    private int currentCorrectPositions;
    public GameObject[] triggerPoints;
    public Text scoreText;
    public TMP_Text puzzleComplete;
    // Start is called before the first frame update
    void Start()
    {
        
        puzzleCompleted = false;
        total = 16;
        currentCorrectPositions = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        currentCorrectPositions = 0;
        for (int i = 0; i < triggerPoints.Length; i++)
        {

            if (triggerPoints[i].GetComponent<CorrectBook>().inCorrectSpot)
            {

                currentCorrectPositions++;
                if (!(triggerPoints[i].GetComponent<CorrectBook>().inCorrectSpot))
                {
                    currentCorrectPositions--;

                }

            }


        }
           

        scoreText.text = "Score: " + currentCorrectPositions;

        if(currentCorrectPositions == total)
        {
           puzzleCompleted = true;
            puzzleComplete.text = "Puzzle Completed!";

        }
    }

    public void ExitPuzzle()
    {
        SceneManager.LoadScene("Room");

    }
}
