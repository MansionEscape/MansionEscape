using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // player data references for all players selected as well as current player
    public PlayerData data, playerOneData, playerTwoData, playerThreeData, playerFourData, playerFiveData, playerSixData;

    //String file names for player files for the 6 player game saves/profiles
    private string playerOneFile = "player1.txt";
    private string playerTwoFile = "player2.txt";
    private string playerThreeFile = "player3.txt";
    private string playerFourFile = "player4.txt";
    private string playerFiveFile = "player5.txt";
    private string playerSixFile = "player6.txt";

    //Selected player represents the profile the player has selected 
    public string selectedPlayer;

    //Booleans to inform if a player profile is empty or currently being used
    public bool playerOneEmpty;
    public bool playerTwoEmpty;
    public bool playerThreeEmpty;
    public bool playerFourEmpty;
    public bool playerFiveEmpty;
    public bool playerSixEmpty;


    //When selected player is saved, write to that file
    public void Save()
    {
        //checks what game save/ profile has been selected and saves to the correct file
        if (selectedPlayer == "playerOne")
        {
            string json = JsonUtility.ToJson(data);
            WriteToFile(playerOneFile, json);

        }
        else if (selectedPlayer == "playerTwo")
        {
            string json = JsonUtility.ToJson(data);
            WriteToFile(playerTwoFile, json);

        }
        else if (selectedPlayer == "playerThree")
        {
            string json = JsonUtility.ToJson(data);
            WriteToFile(playerThreeFile, json);

        }
        else if (selectedPlayer == "playerFour")
        {
            string json = JsonUtility.ToJson(data);
            WriteToFile(playerFourFile, json);

        }
        else if (selectedPlayer == "playerFive")
        {
            string json = JsonUtility.ToJson(data);
            WriteToFile(playerFiveFile, json);

        }
        else if (selectedPlayer == "playerSix")
        {
            string json = JsonUtility.ToJson(data);
            WriteToFile(playerSixFile, json);

        }
    }

    //Loads all profiles 
    public void LoadAll()
    {
        playerOneData = new PlayerData();
        string playerOneJson = ReadFromFile(playerOneFile, ref playerOneEmpty);
        JsonUtility.FromJsonOverwrite(playerOneJson, playerOneData);

        playerTwoData = new PlayerData();
        string playerTwoJson = ReadFromFile(playerTwoFile, ref playerTwoEmpty);
        JsonUtility.FromJsonOverwrite(playerTwoJson, playerTwoData);

        playerThreeData = new PlayerData();
        string playerThreeJson = ReadFromFile(playerThreeFile, ref playerThreeEmpty);
        JsonUtility.FromJsonOverwrite(playerThreeJson, playerThreeData);

        playerFourData = new PlayerData();
        string playerFourJson = ReadFromFile(playerFourFile, ref playerFourEmpty);
        JsonUtility.FromJsonOverwrite(playerFourJson, playerFourData);

        playerFiveData = new PlayerData();
        string playerFiveJson = ReadFromFile(playerFiveFile, ref playerFiveEmpty);
        JsonUtility.FromJsonOverwrite(playerFiveJson, playerFiveData);

        playerSixData = new PlayerData();
        string playerSixJson = ReadFromFile(playerSixFile, ref playerSixEmpty);
        JsonUtility.FromJsonOverwrite(playerSixJson, playerSixData);
    }

    //Loads the data of a singular player
    public void LoadPlayer()
    {
        if (selectedPlayer == "playerOne")
        {
            data = new PlayerData();
            string json = ReadFromFile(playerOneFile, ref playerOneEmpty);
            JsonUtility.FromJsonOverwrite(json, data);
        }
        else if (selectedPlayer == "playerTwo")
        {
            data = new PlayerData();
            string json = ReadFromFile(playerTwoFile, ref playerTwoEmpty);
            JsonUtility.FromJsonOverwrite(json, data);
        }
        else if (selectedPlayer == "playerThree")
        {
            data = new PlayerData();
            string json = ReadFromFile(playerThreeFile, ref playerThreeEmpty);
            JsonUtility.FromJsonOverwrite(json, data);

        }
        else if (selectedPlayer == "playerFour")
        {
            data = new PlayerData();
            string json = ReadFromFile(playerFourFile, ref playerFourEmpty);
            JsonUtility.FromJsonOverwrite(json, data);
        }
        else if (selectedPlayer == "playerFive")
        {
            data = new PlayerData();
            string json = ReadFromFile(playerFiveFile, ref playerFiveEmpty);
            JsonUtility.FromJsonOverwrite(json, data);
        }
        else if (selectedPlayer == "playerSix")
        {
            data = new PlayerData();
            string json = ReadFromFile(playerSixFile, ref playerSixEmpty);
            JsonUtility.FromJsonOverwrite(json, data);

        }


    }

    //Removes item from player inventory after puzzle has been completed.
    public void RemoveItemPuzzleItem(Item item)
    {
        data.items.Remove(item);
        Save();
    }

    //Adds a puzzle item collected in puzzle
    public void AddPuzzleItem(Item item)
    {
        data.items.Add(item);
        Save();
    }

    //Updates the player by saving and then loading the selected player again
    public void UpdatePlayer()
    {
        Save();
        LoadPlayer();
    }

    // If a used game save is selected then this deletes the corresponding one.
    public void DeletePlayer()
    {
        if (selectedPlayer == "playerOne")
        {
            //Calls delete file path method: see method below
            DeleteFile(playerOneFile, playerOneEmpty);
        }
        else if (selectedPlayer == "playerTwo")
        {
            DeleteFile(playerTwoFile, playerTwoEmpty);
        }
        else if (selectedPlayer == "playerThree")
        {
            DeleteFile(playerThreeFile, playerThreeEmpty);
        }
        else if (selectedPlayer == "playerFour")
        {
            DeleteFile(playerFourFile, playerFourEmpty);
        }
        else if (selectedPlayer == "playerFive")
        {
            DeleteFile(playerFiveFile, playerFiveEmpty);
        }
        else if (selectedPlayer == "playerSix")
        {

            DeleteFile(playerSixFile, playerSixEmpty);

        }
    }

    //Checks if the file name given if path exists, deletes that file path and then sets the profile back to empty
    public void DeleteFile(string fileName, bool profileEmpty)
    {
        string path = GetFilePath(fileName);

        if (File.Exists(path))
        {

            File.Delete(path);

            profileEmpty = true;
            Debug.Log("profile Deleted");

        }
        else
        {
            Debug.Log("File Not Found");
        }
    }

    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromFile(string fileName, ref bool playerEmpty)
    {
        string path = GetFilePath(fileName);
        Debug.Log(path);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            playerEmpty = true;
        }
        return "";
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    public void playerSelected(string currentPlayer)
    {
        selectedPlayer = currentPlayer;
        LoadPlayer();
    }


    public void LoadPlayerGame()
    {
        DontDestroyOnLoad(gameObject);
    }
}

