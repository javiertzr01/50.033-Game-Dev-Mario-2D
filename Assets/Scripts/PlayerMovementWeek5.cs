using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovementWeek5 : MonoBehaviour
{
    public GameConstants gameConstants;
    public BoolVariable faceRightState;
    public BoolVariable isInvincible;
    public UnityEvent incrementScore;
    public UnityEvent takeDamage;

    // Movement
    float speed;
    float maxSpeed;
    float upSpeed;
    float deathImpulse;
    private bool moving = false;
    private bool jumpState = false;

    // Position
    private Vector3 spawnLocation;
    // public Vector3 cameraSpawnLocation;
    public bool onGroundState = true;
    // private bool faceRightState = true;
    [System.NonSerialized]
    public bool alive = true;   // To prevent recollision with Goomba

    // Structure
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    public Animator marioAnimator;
    // public Transform gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Set up Movement
        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        upSpeed = gameConstants.upSpeed;
        deathImpulse = gameConstants.deathImpulse;
        faceRightState.value = true;
        spawnLocation = gameConstants.spawnLocations[int.Parse(SceneManager.GetActiveScene().name.Substring(SceneManager.GetActiveScene().name.Length-1)) - 1];

        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponentInChildren<SpriteRenderer>();
        marioAnimator.SetBool("onGround", onGroundState);   // Update animator


    }

    // Movement
    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);
        if (marioBody.velocity.magnitude < maxSpeed)
        {
            marioBody.AddForce(movement * speed);
        }
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    public void Jump()
    {
        if (alive && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);  // Add impulse up
            onGroundState = false;
            jumpState = true;
            marioAnimator.SetBool("onGround", onGroundState);   // Update animator
        }
    }

    public void JumpHold()
    {
        if (alive && onGroundState)
        {
            jumpState = true;
        }
        
    }

    public void JumpStop()
    {
        jumpState = false;
    }

    void FlipMarioSprite(int value)
    {
        // Switching sprite direction
        if (value == -1 && faceRightState.value)
        {
            faceRightState.value = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 2.0f)    // If Mario is turning right abruptly
            {
                marioAnimator.SetTrigger("onSkid"); // Update animator
            }
        }

        else if (value == 1 && !faceRightState.value)
        {
            faceRightState.value = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -2.0f)   // If Mario is turning left abruptly
            {
                marioAnimator.SetTrigger("onSkid"); // Update animator
            }
        }
    }

    private void fallingCheck()
    {
        if (!Physics2D.BoxCast(transform.position, new Vector2(0.88f ,1.0f), 0, -transform.up, 0.5f, collisionLayerMask) && onGroundState)
        {
            onGroundState = false;
            marioAnimator.SetBool("falling", true);   // Update animator
        }
    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));  // Update animator
        fallingCheck();
    }

    int collisionLayerMask = (1 << 3) | (1 << 8);

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((collisionLayerMask & (1 << collision.GetContact(0).collider.gameObject.layer)) > 0) && !onGroundState)
        {
            onGroundState = true;
            jumpState = false;
            marioAnimator.SetBool("onGround", onGroundState);   // Update animator
            marioAnimator.SetBool("falling", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if ((((1 << 9) & (1 << collision.gameObject.layer)) > 0) && alive)
        if ((collision.gameObject.layer == 9 || isInvincible.value) && alive)
        {
            // GameManager.instance.StompGoomba(collision.transform.parent.parent.name);
            collision.transform.parent.parent.GetComponent<EnemyMovementWeek5>().Die();
            incrementScore.Invoke();
            marioBody.AddForce(Vector2.up * upSpeed * 2, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.CompareTag("Enemy") && alive)
        {
            // Play death animation
            takeDamage.Invoke();
        }
    }

    public void DeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
        alive = false;
    }

    public void DamageMario()
    {
        GetComponentInChildren<MarioStateController>().SetPowerup(PowerupType.Damage);
    }


    // FixedUpdate is used for Physics Logic
    void FixedUpdate()
    {
    // Control Mario's Movements
        if(alive && moving)
        {
            Move(faceRightState.value == true ? 1 : -1);
        }
        if (jumpState)
        {
            marioBody.AddForce(Vector2.up * upSpeed * 5.1f, ForceMode2D.Force);
        }
    }

    // GameOver and GameRestarts
    public void GameRestart()
    {
        GetComponentInChildren<MarioStateController>().GameRestart();
        marioBody.transform.position = spawnLocation;    // Reset Mario Position
        faceRightState.value = true;  // Reset Sprite Direction
        marioSprite.flipX = false;  // Reset Sprite Direction
        marioAnimator.SetTrigger("gameRestart");    // Reset Animation
        alive = true;
        // gameCamera.position = cameraSpawnLocation; // Reset Camera Position
    }
}
