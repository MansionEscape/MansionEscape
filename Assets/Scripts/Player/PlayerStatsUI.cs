using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    public MainController player;
    public TMP_Text nameText;
    public TMP_Text currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = player.currentPlayer.data.playerName;
        currentLevel.text = player.currentPlayer.data.currentLevel.ToString();
    }

}
