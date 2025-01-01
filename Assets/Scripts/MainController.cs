using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;


public class MainController : MonoBehaviour
{
    public SpawnPlayer spawnPlayer;
    public GameObject Player;

    public GameObject playerManagerObject;
    public PlayerManager currentPlayer;

    public GameObject welcomeWindow;
    public GameObject pauseWindow;
    public TMP_Text welcomePlayerName;
    //ROOMS 

    
    void Awake()
    {
        playerManagerObject = GameObject.Find("PlayerManager");
        currentPlayer = playerManagerObject.GetComponent<PlayerManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentPlayer.LoadPlayer();

        if(!currentPlayer.data.firstTimePlaying)
        {
            welcomePlayerName.text = currentPlayer.data.playerName;
            welcomeWindow.SetActive(true);
            currentPlayer.data.firstTimePlaying = true;
            currentPlayer.data.currentRoom = "Entrance";
            currentPlayer.data.mansionLevel = "Ground";
            currentPlayer.Save();

        }
        else if(currentPlayer.data.currentRoom != "Entrance")
        {
            spawnPlayer.SpawnPoint(currentPlayer.data.mansionLevel, currentPlayer.data.currentRoom);
        }

    }



    public void UpdateCurrentPlayerRoom(string roomName)
    {
        currentPlayer.data.currentRoom = roomName;
        currentPlayer.UpdatePlayer();
    }

    public void PauseGame()
    {
        pauseWindow.SetActive(true);

    }

    public void ResumeGame()
    {
        pauseWindow.SetActive(false);
    }

    public void ExitGame()
    {
        currentPlayer.Save();
        Destroy(this.playerManagerObject);
        SceneManager.LoadScene("StartMenu");
    }

    
}
