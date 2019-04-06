﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    private int score;
    private int lives;
    private string WIN_MESSAGE = "You Win!! ";
    private string LOSE_MESSAGE = "You Lose!! :( ";
    private string START_MESSAGE = "Press Space/A to Start ";
    private string PAUSE_MESSAGE = "Press Space/A to unpause ";
    private string LIVES_PREFIX = "Lives: ";
    private string SCORE_PREFIX = "Score: ";
    private enum STATE {Running, Over, Paused};
    private STATE currentState;
    private SpawnBall spawner;

    public Text scoreText;
    public Text livesText;
    public Text messageText;
    public int startingLives;
    public int targetScore;
    
    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<SpawnBall>();
        LoseGame();
        messageText.text = START_MESSAGE;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case STATE.Running:
                if (Input.GetButtonUp("Cancel"))
                {
                    PauseGame();
                }
                break;
            case STATE.Paused:
            case STATE.Over:
                if (Input.GetButtonUp("Submit"))
                {
                    UnPauseGame();
                }
                break;
        }
    }

    public void NewGame()
    {
        lives = startingLives;
        score = 0;
        UpdateLivesText();
        UpdateScoreText();
        messageText.text = "";
        Time.timeScale = 1.0f;
        currentState = STATE.Running;
        spawner.Spawn();
    }

    public void PauseGame()
    {
        currentState = STATE.Paused;
        messageText.text = PAUSE_MESSAGE;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        if (currentState == STATE.Over)
        {
            NewGame();
        }
        else
        {
            currentState = STATE.Running;
            messageText.text = "";
            Time.timeScale = 1.0f;
        }
    }

    public void WinGame()
    {
        Destroy(spawner.Ball);
        Time.timeScale = 0;
        messageText.text = WIN_MESSAGE + START_MESSAGE;
        currentState = STATE.Over;
    }
    public void LoseGame()
    {
        Time.timeScale = 0;
        ZeroScore();
        messageText.text = LOSE_MESSAGE + START_MESSAGE;
        currentState = STATE.Over;
    }

    public void IncrementLives()
    {
        lives += 1;
        UpdateLivesText();
        CheckVictory();
    }

    public void DecrementLives()
    {
        lives -= 1;
        UpdateLivesText();
        CheckVictory();
        if (currentState != STATE.Over)
        {
            spawner.Spawn();
        }
    }

    public void ZeroScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public void IncrementScore()
    {
        score += 1;
        UpdateScoreText();
        CheckVictory();
    }

    public void DecrementScore()
    {
        score -= 1;
        UpdateScoreText();
        CheckVictory();
    }

    public void UpdateScoreText()
    {
        scoreText.text = SCORE_PREFIX + score.ToString();
    }

    public void UpdateLivesText()
    {
        livesText.text = LIVES_PREFIX + lives.ToString();
    }

    public void CheckVictory()
    {
        if (score >= targetScore)
        {
            WinGame();
        } else if (lives <= 0)
        {
            LoseGame();
        }
    }
}
