using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : BasePowerup
{
    // Start is called before the first frame update
    private Vector3 ogPos;
    private AudioSource starAudio;
    protected override void Start()
    {
        base.Start(); // Call base class Start()
        this.type = PowerupType.StarMan;
        ogPos = transform.position;
        starAudio = GetComponent<AudioSource>();
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
        starAudio.PlayOneShot(starAudio.clip);
        PlaySpawnAnimation();
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void MovePowerup()
    {
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse);
    }

    public override void ApplyPowerup(MonoBehaviour i)
    {
        Debug.Log("Starman implementation required");
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
        this.GetComponent<BoxCollider2D>().enabled = false;
        rigidBody.bodyType = RigidbodyType2D.Static;
        this.gameObject.GetComponentInChildren<Animator>().SetTrigger("gameRestart");
    }
}
