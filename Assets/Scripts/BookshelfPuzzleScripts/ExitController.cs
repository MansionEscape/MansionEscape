using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExitPuzzle()
    {
        SceneManager.LoadScene("Room");

    }
}
