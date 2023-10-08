using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickPowerupController : MonoBehaviour, IPowerupController
{
    private BasePowerup powerup;
    private bool powerupAvailable = false;
    private Animator brickAnimator;
    // Start is called before the first frame update
    void Start()
    {
        brickAnimator = GetComponent<Animator>();
        CheckPowerup();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        brickAnimator.Play("brick-bounce");
        brickAnimator.SetTrigger("bounce");
        if (powerupAvailable)
        {
            powerup.SpawnPowerup();
        }
    }

    public void Disable(){}


    // Helper Function

    void CheckPowerup()
    {
        if (transform.parent.GetComponentInChildren<BasePowerup>() != null)
        {
            powerup = transform.parent.GetComponentInChildren<BasePowerup>();
            powerupAvailable = true;
        }
    }
}
