using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextSceneName;
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
            GameManager.instance.setLevelStartingScore();
        }
    }
}
