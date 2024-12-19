using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Controls when the puzzle is complete by tracking if books are in the correct positions.
public class PuzzleCompleteController : MonoBehaviour
{

    [System.NonSerialized] public bool puzzleCompleted;
    private int total; //  total number of puzzles
    private int currentCorrectPositions; //how many books the player has in the correct positions
    public GameObject[] triggerPoints; // List of game obejcts which are the trigger points underneath the bookshelf.
    public Text scoreText; // score text on the bookshelf puzzle
    public TMP_Text puzzleComplete; // puzzle complete texts


    // Start is called before the first frame update
    void Start()
    {
        puzzleCompleted = false; // puzzle completed is set to false
        total = 16; // total is set to 16 - no of books that need to be rearranged
        currentCorrectPositions = 0; // number of books the player has correct is set to 0
        
    }

    // Update is called once per frame
    void Update()
    {
        
        currentCorrectPositions = 0;
        //cycles through the trigger points in the list
        for (int i = 0; i < triggerPoints.Length; i++)
        {
            //checks if the boolean within the "Correct Book" script is true 
            if (triggerPoints[i].GetComponent<CorrectBook>().inCorrectSpot)
            {
                //if true then inrement the currenrtCorrectPositions 
                currentCorrectPositions++;

                //wile the book has enrtered the correct spot, keep track is the book moves from its potion
                if (!(triggerPoints[i].GetComponent<CorrectBook>().inCorrectSpot))
                {
                    //if the book is moved from the correct spot, decrease the score.
                    currentCorrectPositions--;

                }

            }


        }
           
        //update the score text with the current correct positions variable
        scoreText.text = "Score: " + currentCorrectPositions;

        // check if the current correct positions by the player equals the total number of correct books
        if(currentCorrectPositions == total)
        {
            //if true the puzzle is now complete and set the text to let the playerr know
           puzzleCompleted = true;
            puzzleComplete.text = "Puzzle Completed!";

        }
    }

    public void ExitPuzzle()
    {
        SceneManager.LoadScene("Room");

    }
}
