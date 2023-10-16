using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerWeek5 : BasePowerupWeek5
{
    // Start is called before the first frame update
    private Vector3 ogPos;
    private AudioSource fireFlowerAudio;
    protected override void Start()
    {
        base.Start(); // Call base class Start()
        type = PowerupType.FireFlower;
        ogPos = transform.position;
        fireFlowerAudio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // TODO: Do something when colliding with player
            PowerupCollected(this);
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
        fireFlowerAudio.PlayOneShot(fireFlowerAudio.clip);
        PlaySpawnAnimation();
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public override void ApplyPowerup(MonoBehaviour i)
    {
        // try
        MarioStateController mario;
        bool result = i.TryGetComponent<MarioStateController>(out mario);
        if (result)
        {
            mario.SetPowerup(powerupType);
        }
    }

    public override void GameRestart()
    {
        base.GameRestart();
        Reset();
    }

    // Helper Functions
    void PlaySpawnAnimation()
    {
        gameObject.GetComponentInChildren<Animator>().SetTrigger("spawned");
    }

    void Reset()
    {
        // gameObject.SetActive(true);
        transform.position = ogPos;
        GetComponent<BoxCollider2D>().enabled = false;
        rigidBody.bodyType = RigidbodyType2D.Static;
        gameObject.GetComponentInChildren<Animator>().SetTrigger("gameRestart");
    }
}
