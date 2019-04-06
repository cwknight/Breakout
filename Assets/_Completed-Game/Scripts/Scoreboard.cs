using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    private int score;
    private int lives;
    private string WIN_MESSAGE = "You Win!!";
    private string LOSE_MESSAGE = "You Lose!! :( ";
    private string START_MESSAGE = "Press Any Button to Start";
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
        NewGame();  
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGame()
    {
        lives = startingLives;
        score = 0;
        UpdateLivesText();
        UpdateScoreText();
        messageText.text = "";
        currentState = STATE.Running;
        spawner.Spawn();
    }

    public void PauseGame()
    {
        currentState = STATE.Paused;
        messageText.text = START_MESSAGE;
        //need to implement pause. Possibly Time.timeScale = 0;
    }

    public void WinGame()
    {
        Destroy(spawner.Ball);
        messageText.text = WIN_MESSAGE;
        currentState = STATE.Over;
    }
    public void LoseGame()
    {
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
