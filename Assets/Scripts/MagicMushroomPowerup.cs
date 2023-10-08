using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class MagicMushroomPowerup : BasePowerup
{
    // Start is called before the first frame update
    private Vector3 ogPos;
    protected override void Start()
    {
        base.Start(); // Call base class Start()
        this.type = PowerupType.MagicMushroom;
        ogPos = transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // TODO: Do something when colliding with player
            // Then destroy powerup
            DestroyPowerup();
        }
        else if (col.gameObject.layer == 7)
        {
            if (spawned)
            {
                goRight = !goRight;
                rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);
            }
        }
    }

    //interface implementation
    public override void SpawnPowerup()
    {
        base.SpawnPowerup();
        PlaySpawnAnimation();
    }

    public void MovePowerup()
    {
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse);
    }

    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object
    }

    public override void GameRestart()
    {
        base.GameRestart();
        Reset();
    }

    // Helper Functions
    void PlaySpawnAnimation()
    {
        this.gameObject.GetComponentInChildren<Animator>().SetTrigger("spawned");
    }

    void Reset()
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.position = ogPos;
        this.gameObject.GetComponentInChildren<Animator>().SetTrigger("gameRestart");
        rigidBody.velocity = new Vector2(0.0f, 0.0f);
    }
}
