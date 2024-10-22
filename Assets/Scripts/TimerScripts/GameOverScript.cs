using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public NewBehaviourScript timerScript; // So that we can interact with the timer
    //public GameObject player; 
    //public PlayerController playerController; 

    private bool gameOver;

    //Sets Game Menu to false
    private void Start()
    {
        gameObject.SetActive(false);
        gameOver = false;
    }

    // Displays the Game Over Menu to the screen
    public void displayGameOverScreen()
    {
        //Debug.Log(timerScript.returnCurrentTime());
        if (timerScript.returnCurrentTime() <= 0)
        {
            gameOver = true;
            gameObject.SetActive(true);
        }
    }

    // If user selects retry
    public void RestartLevel()
    {
        timerScript.ResetTimer(); // The timer is reset
        gameOver = false;
        gameObject.SetActive(false); // Game menu is unselected

        //player.transform.position = new Vector3(0, 4.96f, 0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
}
