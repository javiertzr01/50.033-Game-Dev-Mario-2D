using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private Vector3[] scoreTextPosition = {
        new Vector3(-764, 460, 0),
        new Vector3(-106, 0, 0)
        };
    
    private Vector3[] restartButtonPosition = {
        new Vector3(861, 465, 0),
        new Vector3(0, -190, 0)
        };

    public GameObject scoreText;
    public Transform restartButton;
    
    public GameObject gameOverCanvas;

    public GameObject highscoreText;
    public IntVariable gameScore;

    void Awake()
    {
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameRestart.AddListener(GameStart);
        GameManager.instance.gameOver.AddListener(GameOver);
        GameManager.instance.scoreChange.AddListener(SetScore);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        gameOverCanvas.SetActive(false);    // Hide GameOverCanvas
        scoreText.transform.localPosition = scoreTextPosition[0];
        restartButton.localPosition = restartButtonPosition[0];
    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "SCORE: " + score.ToString();
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);     // Show GameOverCanvas
        scoreText.transform.localPosition = scoreTextPosition[1];
        restartButton.transform.localPosition = restartButtonPosition[1];
        // highscoreText.SetActive(true);      // Show Highscore Text
        highscoreText.GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
        AudioSource gameOverMusic = GameObject.Find("Game Over Canvas").GetComponent<AudioSource>();
        gameOverMusic.PlayOneShot(gameOverMusic.clip);
    }
}
