using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerData
{
    public string playerName = "";

    public bool firstTimePlayingComplete = false;
    public bool firstMovement = false;
    public bool tutorialComplete = false;

    public int progressionPercentage = 0;

    public string mansionLevel = "";
    public string currentRoom = "";
    public int currentLevel = 0;

    public List<Item> items = new List<Item>();

    public bool levelOneComplete = false;
    public bool levelTwoComplete = false;
    public bool levelThreeComplete = false;

    public bool ObjectivePuzzleOneComplete = false;
    public bool ObjectivePuzzleTwoComplete = false;
    public bool ObjectivePuzzleThreeComplete = false;



}
