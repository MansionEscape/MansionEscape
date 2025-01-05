using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Method to load a specific scene
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //private string previousScene;

    //void Start()
    //{
    //    previousScene = PlayerPrefs.GetString("PreviousScene", "MainMenu");
    //}

    //public void GoBack()
    //{
    //    SceneManager.LoadScene(previousScene);
    //}

    //public static void SaveCurrentScene()
    //{
    //    PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    //}
}
