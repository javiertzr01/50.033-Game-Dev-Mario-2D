using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    public UnityEvent<string> goombaDie;

    public IntVariable gameScore;
    public int levelStartingScore;

    // Start is called before the first frame update
    void Start()
    {
        gameScore.value = 0;
        setLevelStartingScore();
        gameStart.Invoke();
        Time.timeScale = 1.0f;
        SceneManager.activeSceneChanged += SceneSetup;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLevelStartingScore()
    {
        levelStartingScore = gameScore.value;
    }

    public void GameRestart()
    {
        gameScore.value = levelStartingScore;
        SetScore(gameScore.value);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(increment);
        SetScore(gameScore.value);
    }

    public void SetScore(int score)
    {
        scoreChange.Invoke(score);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameScore.SetValue(gameScore.value);
        gameOver.Invoke();
    }

    public void StompGoomba(string name)
    {
        goombaDie.Invoke(name);
        IncreaseScore(1);
    }

    public void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore(gameScore.value);
    }
}
