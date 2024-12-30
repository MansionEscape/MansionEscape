using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public GameObject chest;
    public GameObject chestTrigger;

    // Start is called before the first frame update
     void Start()
    {
        //Checks if the object assigned is the StartMenuUI
        if (gameObject.name == "StartMenuUI")
        {
            //if true the start menu on start will be displayed
            gameObject.SetActive(true);
        } else
        {
            //any other game objects will be hidden on start
            gameObject.SetActive(false);
        }
        
    }

    //Pause Game sets Pause Menu panel to active
    public void PauseGame()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }

    //Restart level function by loading the active scene.
    public void RestartLevel()
    {
        GameState.isPuzzleCompleted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Exits level and loads the start menu scene
    public void ExitLevel()
    {
        SceneManager.LoadScene("StartMenu");
    }
  
    //Exit application from start menu
    public void ExitGame()
    {
        Application.Quit();
    }

  
}
