using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    private BasePowerup powerup;
    private Rigidbody2D qbBody;
    private Animator qbAnimator;

    void Awake()
    {
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }

    void Start()
    {
        powerup = transform.parent.parent.GetComponentInChildren<BasePowerup>();
        qbBody = this.GetComponent<Rigidbody2D>();
        qbAnimator = this.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !powerup.hasSpawned)
        {
            qbAnimator.SetBool("collected", true);    // QuestionBox animation
            powerup.SpawnPowerup();
        }
    }

    public void Disable()
    {
        qbBody.bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    void Enable()
    {
        qbBody.bodyType = RigidbodyType2D.Dynamic;
    }

    void GameRestart()
    {
        Enable();
        qbAnimator.SetTrigger("gameRestart");
        qbAnimator.SetBool("collected", false);
    }
}
