using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour
{
    public UnityEvent gamePause;
    public UnityEvent gameResume;

    public Sprite pauseIcon;
    public Sprite playIcon;
    private Image image;

    private bool isPaused = false;
    
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void ButtonClick()
    {
        Time.timeScale = isPaused ? 1.0f : 0.0f;
        isPaused = !isPaused;
        if (isPaused)
            gamePause.Invoke();
        else
            gameResume.Invoke();
    }

    public void Pause()
    {
        image.sprite = playIcon;
        GetComponentInParent<AudioSource>().Pause();
    }

    public void Resume()
    {
        image.sprite = pauseIcon;
        GetComponentInParent<AudioSource>().UnPause();
    }
}
