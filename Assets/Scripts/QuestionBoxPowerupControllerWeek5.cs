using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxPowerupControllerWeek5 : MonoBehaviour, IPowerupController
{
    private BasePowerupWeek5 powerup;
    private Rigidbody2D qbBody;
    private Animator qbAnimator;

    void Start()
    {
        qbBody = this.GetComponent<Rigidbody2D>();
        qbAnimator = this.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        powerup = transform.parent.parent.GetComponentInChildren<BasePowerupWeek5>();
        if (other.gameObject.tag == "Player" && powerup != null && !powerup.hasSpawned)
        // if (other.gameObject.tag == "Player")
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

    public void GameRestart()
    {
        Enable();
        qbAnimator.SetTrigger("gameRestart");
        qbAnimator.SetBool("collected", false);
    }
}
