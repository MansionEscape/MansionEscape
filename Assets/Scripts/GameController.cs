using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    private string sceneName = "Room";

    // Start is called before the first frame update
    private void Start()
    {
        if (gameObject.name == "StartMenuUI")
        {
            gameObject.SetActive(true);
        } else
        {
            gameObject.SetActive(false);
        }
        
    }

    public void PauseGame()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("StartMenu");
    }
  
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
