using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Scoreboard : MonoBehaviour
{
    public enum Difficulties
    {
        Easy = 1,
        Medium = 3,
        Hard = 5,
        Random = 0
    }

    private int score;
    private int lives;
    private string WIN_MESSAGE = "You Win!! ";
    private string LOSE_MESSAGE = "You Lose!! :( ";
    private string START_MESSAGE = "Press Space/A to Start ";
    private string PAUSE_MESSAGE = "Press Space/A to unpause ";
    private string LIVES_PREFIX = "Lives: ";
    private string SCORE_PREFIX = "Score: ";
    private string LEVEL_PREFIX = "Level: ";
    private enum STATE { Running, Over, Paused, Started, Lost };
    private STATE currentState;
    private SpawnBall ballSpawner;
    private SpawnBrick brickSpawner;
    private int rowPosition;
    private int levelCount;
    private int targetScore = 0;

    public RawImage titleImage;
    public Text scoreText;
    public Text livesText;
    public Text levelText;
    public Text messageText;
    public int startingLives;
    public int StartingLevel = 0;
    public int BrickHealth;
    public GameObject OptionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        ballSpawner = GetComponent<SpawnBall>();
        brickSpawner = GetComponent<SpawnBrick>();
        LoseGame();
        titleImage.GetComponent<RawImage>().enabled = true;
        messageText.enabled = true;
        OptionsMenu.SetActive(false); 
        messageText.text = START_MESSAGE;
        currentState = STATE.Started;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) {
            case STATE.Running:
                if (Input.GetButtonUp("Cancel")) {
                    PauseGame();
                }
                break;
            case STATE.Started:
            case STATE.Paused:
            case STATE.Lost:
            case STATE.Over:
                if (Input.GetButtonUp("Submit")) {
                    UnPauseGame();
                }
                break;
        }
    }

    public void NewGame()
    {
        score = 0;
        targetScore = 0;
        brickSpawner.DestroyBricks();
        UpdateLivesText();
        UpdateScoreText();
        messageText.enabled = false;
        OptionsMenu.SetActive(false);
        Time.timeScale = 1.0f;
        currentState = STATE.Running;
        ballSpawner.Spawn();
        SpawnLevel(levelCount, BrickHealth);
        
    }

    public void SetDifficulty(string difficulty)
    {
        Difficulties difficultyEnum = (Difficulties) Enum.Parse(typeof(Difficulties), difficulty);
        BrickHealth = (int) difficultyEnum;
    }

    public void ToggleContinuous()
    {
        if (StartingLevel == 0) {
            StartingLevel = 1;
        } else {
            StartingLevel = 0;
        }
    }

    public void SetStartingHealth(int startingHealth)
    {
        startingLives = startingHealth;
    }

    public void SpawnLevel(int level)
    {
        UpdateLevelText(level);
        brickSpawner.SpawnLevel(level);

    }

    public void SpawnLevel(int level, int health)
    {
        UpdateLevelText(level);
        brickSpawner.SpawnLevel(level, health);
    }
    public void PauseGame()
    {

        currentState = STATE.Paused;
        messageText.text = PAUSE_MESSAGE;
        messageText.enabled = true;
        if (currentState != STATE.Over) {
            OptionsMenu.SetActive(false);
        } else {
            OptionsMenu.SetActive(true);
        }
        
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        if (currentState == STATE.Started) {
            titleImage.GetComponent<RawImage>().enabled = false;
            currentState = STATE.Lost;
            OptionsMenu.SetActive(true);
        } else if (currentState == STATE.Lost) {
            levelCount = StartingLevel;
            NewGame();
        } else if (currentState == STATE.Over) {
            NewGame();
        } else {
            currentState = STATE.Running;
            messageText.enabled = false;
            OptionsMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void WinGame()
    {
        Destroy(ballSpawner.Ball);
        Time.timeScale = 0;
        levelCount++;
        messageText.text = WIN_MESSAGE + START_MESSAGE;
        messageText.enabled = true;
        currentState = STATE.Over;
    }
    public void LoseGame()
    {
        Time.timeScale = 0;
        ZeroScore();
        levelCount = StartingLevel;
        lives = startingLives;
        brickSpawner.DestroyBricks();
        Destroy(ballSpawner.Ball);
        messageText.text = LOSE_MESSAGE + START_MESSAGE;
        messageText.enabled = true;
        OptionsMenu.SetActive(true);
        currentState = STATE.Lost;
    }

    public void ZeroLives()
    {
        lives = 0;
        UpdateLivesText();
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
        if (currentState != STATE.Over) {
            ballSpawner.Spawn();
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

    public void UpdateLevelText(int level)
    {
        levelText.text = LEVEL_PREFIX + level.ToString();
    }

    public void CheckVictory()
    {
        if (score >= brickSpawner.brickCount) {
            WinGame();
        } else if (lives <= 0) {
            LoseGame();
        }
    }
}
