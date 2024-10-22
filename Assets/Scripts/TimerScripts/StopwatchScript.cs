using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class NewBehaviourScript : MonoBehaviour
{
    public GameOverScript gameOverScript; // So that we can access game over parameters
    private static NewBehaviourScript instance; // This needed so that the timer can be constant throughout scenes
    private bool timerActive;
    private float currentTime;
    [SerializeField] private float totalMinutes; // You can select how many minutes, currently set in game to 0.2 (12 seconds)
    private bool hasMoved; // To check if player has moved
    [SerializeField] private TMP_Text text; // Timer text
    public int livesCounter; 
    public Text lives; // lives text


    //This keeps the timer active even throughout all scenes
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentTime = totalMinutes * 60;
        hasMoved = false;
        lives.text = "Lives:" + livesCounter;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasMoved && IsPlayerMoving())
        {
            StartTimer();  // Start the timer when the player moves for the first time
            hasMoved = true;  
        }

        if (timerActive)
        {
            currentTime -= Time.deltaTime; // Time runs backwards

            //If the timer hits 0
            if (currentTime <= 0)
            {
                timerActive = false;
                gameOverScript.displayGameOverScreen();

                //This can be changed if we want lives to not be connected to the timer
                if (livesCounter > 0)
                {
                    livesCounter--;
                    lives.text = "Lives:" + livesCounter;
                }
                //Haven't set anything to happen yet for if the lives hit 0
                //else
                //{

                //}

            }
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        text.text = time.Minutes.ToString("D2") + ":" + time.Seconds.ToString("D2") + ":" + (time.Milliseconds / 10).ToString("D2");
        
    }

    private bool IsPlayerMoving()
    {
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer() 
    {
        timerActive = false;
    }

    public float returnCurrentTime()
    {
        return currentTime;
    }

    public void ResetTimer()
    {
        currentTime = totalMinutes * 60;
        timerActive = false;
        hasMoved = false;
    }

    //public void ResetLives()
    //{
    //    livesCounter
    //}

    public int GetLivesCounter()
    {
        return livesCounter;
    }
}
