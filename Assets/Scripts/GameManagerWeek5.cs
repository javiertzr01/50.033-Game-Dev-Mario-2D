using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManagerWeek5 : MonoBehaviour
{
    public UnityEvent gameStart;
    public IntVariable gameScore;
    private int levelStartingScore;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "World 1-1")
        {
            gameScore.value = 0;
        }
        levelStartingScore = gameScore.value;
        gameStart.Invoke();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(increment);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameScore.SetValue(gameScore.value);
    }

    public void GameRestart()
    {
        gameScore.value = levelStartingScore;
        Time.timeScale = 1.0f;
    }
}
