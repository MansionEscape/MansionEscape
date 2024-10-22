using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false);
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
}
