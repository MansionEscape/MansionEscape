using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GetProfiles : MonoBehaviour
{
    public TMP_Text profileOneText;
    public TMP_Text profileTwoText;
    public TMP_Text profileThreeText;
    public TMP_Text profileFourText;
    public TMP_Text profileFiveText;
    public TMP_Text profileSixText;


    public GameObject newGamePage;
    public GameObject profilePage;

    public TMP_Text playerProfileName;
    public TMP_Text playerCurrentLevel;
    public TMP_Text playerCurrentRoom;
    public TMP_Text playerCurrentMansionLevel;

    public PlayerManager playerManager;

    private bool profileOneNew;
    private bool profileTwoNew;
    private bool profileThreeNew;
    private bool profileFourNew;
    private bool profileFiveNew;
    private bool profileSixNew;

    void Start()
    {
        LoadProfiles();

    }

    public void LoadProfiles()
    {
        playerManager.LoadAll();
        if (playerManager.playerOneEmpty)
        {
            profileOneText.text = "New Game";
            profileOneNew = true;
        }
        else
        {
            profileOneText.text = playerManager.playerOneData.playerName;
        }

        if (playerManager.playerTwoEmpty)
        {
            profileTwoText.text = "New Game";
            profileTwoNew = true;
        }
        else
        {
            profileTwoText.text = playerManager.playerTwoData.playerName;
        }

        if (playerManager.playerThreeEmpty)
        {
            profileThreeText.text = "New Game";
            profileThreeNew = true;
        }
        else
        {
            profileThreeText.text = playerManager.playerThreeData.playerName;
        }

        if (playerManager.playerFourEmpty)
        {
            profileFourText.text = "New Game";
            profileFourNew = true;
        }
        else
        {
            profileFourText.text = playerManager.playerFourData.playerName;
        }

        if (playerManager.playerFiveEmpty)
        {
            profileFiveText.text = "New Game";
            profileFiveNew = true;
        }
        else
        {
            profileFiveText.text = playerManager.playerFiveData.playerName;
        }

        if (playerManager.playerSixEmpty)
        {
            profileSixText.text = "New Game";
            profileSixNew = true;
        }
        else
        {
            profileSixText.text = playerManager.playerSixData.playerName;
        }
    }

    public void BackButton()
    {
        gameObject.SetActive(true);
        profilePage.SetActive(false);
        playerManager.LoadAll();
        playerManager.playerSelected(null);
        Debug.Log(playerManager.selectedPlayer);

    }

    public void ResumeGame()
    {
        playerManager.LoadPlayerGame();
        SceneManager.LoadScene("Mansion 1");
    }

    public void DeleteProfile()
    {
        playerManager.DeletePlayer();
        gameObject.SetActive(true);
        profilePage.SetActive(false);
        LoadProfiles();
    }

    public void DisplayProfile()
    {
        gameObject.SetActive(false);
        profilePage.SetActive(true);
        playerProfileName.text = playerManager.data.playerName;
        playerCurrentLevel.text = playerManager.data.currentLevel.ToString();
        playerCurrentRoom.text = playerManager.data.currentRoom;
        playerCurrentMansionLevel.text = playerManager.data.mansionLevel;
        
    }

    public void SelectProfileOne()
    {
        if (profileOneNew)
        {
            profileOneNew = false;
            gameObject.SetActive(false);
            newGamePage.SetActive(true);
            playerManager.playerSelected("playerOne");
            Debug.Log(playerManager.selectedPlayer);


        }
        else
        {
            playerManager.playerSelected("playerOne");
            Debug.Log(playerManager.selectedPlayer);
            DisplayProfile();

        }
    }
    public void SelectProfileTwo()
    {
        if (profileTwoNew)
        {
            profileTwoNew = false;
            gameObject.SetActive(false);
            newGamePage.SetActive(true);
            playerManager.playerSelected("playerTwo");

        }
        else
        {
            playerManager.playerSelected("playerTwo");
            Debug.Log(playerManager.selectedPlayer);

            DisplayProfile();
        }
    }
    public void SelectProfileThree()
    {
        if (profileThreeNew)
        {
            profileThreeNew = false;
            gameObject.SetActive(false);
            newGamePage.SetActive(true);
            playerManager.playerSelected("playerThree");

        }
        else
        {
            playerManager.playerSelected("playerThree");
            Debug.Log(playerManager.selectedPlayer);

            DisplayProfile();
        }
    }

    public void SelectProfileFour()
    {
        if (profileFourNew)
        {
            profileFourNew = false;
            gameObject.SetActive(false);
            newGamePage.SetActive(true);
            playerManager.playerSelected("playerFour");

        }
        else
        {
            playerManager.playerSelected("playerFour");
            Debug.Log(playerManager.selectedPlayer);

            DisplayProfile();
        }
    }

    public void SelectProfileFive()
    {
        if (profileFiveNew)
        {
            profileFiveNew = false;
            gameObject.SetActive(false);
            newGamePage.SetActive(true);
            playerManager.playerSelected("playerFive");

        }
        else
        {
            playerManager.playerSelected("playerFive");
            Debug.Log(playerManager.selectedPlayer);

            DisplayProfile();
        }
    }

    public void SelectProfileSix()
    {
        if (profileSixNew)
        {
            profileSixNew = false;
            gameObject.SetActive(false);
            newGamePage.SetActive(true);
            playerManager.playerSelected("playerSix");

        }
        else
        {
            playerManager.playerSelected("playerSix");
            Debug.Log(playerManager.selectedPlayer);

            DisplayProfile();
        }
    }

}
