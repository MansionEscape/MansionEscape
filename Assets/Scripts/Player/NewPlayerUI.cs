using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NewPlayerUI : MonoBehaviour
{
    public PlayerManager playerManager;

    public GameObject startMenu;
    public GameObject newPlayerPage;

    public TMP_InputField playerFullName;

    public TMP_Text responseText;

    private void Start()
    {
        Debug.Log(playerManager.selectedPlayer);

    }
    public void CreatePlayerName()
    {

        playerManager.data.playerName = playerFullName.text;

    }


    public void SaveNewPlayer()
    {


        if (playerFullName.text != "")
        {
            
                playerManager.Save();
                playerManager.LoadAll();
            

        }
        else
        {

            responseText.text = "Please Fill Out The 'Sign Here' Field";
            responseText.color = Color.red;

        }






    }


}
