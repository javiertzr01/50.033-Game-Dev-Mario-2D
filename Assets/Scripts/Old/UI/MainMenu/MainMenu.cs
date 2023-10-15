using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public IntVariable gameScore;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    void ButtonUnclick()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    void UpdateText()
    {
        GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
        ButtonUnclick();
    }

    public void ResetHighScore()
    {
        gameScore.ResetHighestValue();
        UpdateText();
        ButtonUnclick();
    }

    
}
