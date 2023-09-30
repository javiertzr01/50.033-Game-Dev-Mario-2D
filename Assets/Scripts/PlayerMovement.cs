using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    public float speed = 55;
    public float maxSpeed = 65;
    public float upSpeed = 35;
    public float deathImpulse = 60;
    private bool moving = false;
    private bool jumpState = false;

    // Position
    public bool onGroundState = true;
    private bool faceRightState = true;
    [System.NonSerialized]
    public bool alive = true;   // To prevent recollision with Goomba

    // Structure
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    public JumpOverGoomba jumpOverGoomba;
    public Animator marioAnimator;
    private GameObject[] questionBlocks;
    private GameObject[] coins;
    public Transform gameCamera;
    // public LayerMask platformLayerMask;

    // UI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI GameOverScoreText;
    public GameObject GameOverCanvas;
    public GameObject enemies;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponentInChildren<SpriteRenderer>();
        marioAnimator.SetBool("onGround", onGroundState);   // Update animator
        GameOverCanvas.SetActive(false);
    }

    void FlipMarioSprite(int value)
    {
        // Switching sprite direction
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 2.0f)    // If Mario is turning right abruptly
            {
                marioAnimator.SetTrigger("onSkid"); // Update animator
            }
        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -2.0f)   // If Mario is turning left abruptly
            {
                marioAnimator.SetTrigger("onSkid"); // Update animator
            }
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
            marioAnimator.SetBool("onGround", onGroundState);   // Update animator
            marioAnimator.SetBool("falling", false);
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

    public void GameOverScene()
    {
        GameOverScoreText.text = scoreText.text;
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void DeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && alive) 
        {
            Debug.Log("Collided with goomba!");

            // Play death animation
            marioAnimator.Play("mario-die");
            marioSprite.GetComponent<PlayerAnimationEffects>().PlayDeathSound();
            alive = false;
        }
    }

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
        if (alive && jumpState)
        {
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpState = false;
        }
    }

    // FixedUpdate is used for Physics Logic
    void FixedUpdate()
    {
    // Control Mario's Movements
        if(alive && moving)
        {
            Move(faceRightState ==  true ? 1 : -1);
        }
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart");
        ResetGame();
        Time.timeScale = 1.0f;
    }

    public void ResetGame()
    {
        marioBody.transform.position = new Vector3(-8.26f, -4.22f, 0.0f);    // Reset Mario Position
        faceRightState = true;  // Reset Sprite Direction
        marioSprite.flipX = false;  // Reset Sprite Direction
        scoreText.text = "SCORE: 0";    // Reset Score
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }

        jumpOverGoomba.score = 0;   // Reset Score
        GameOverCanvas.SetActive(false);

        marioAnimator.SetTrigger("gameRestart");    // Reset Animation

        coins = GameObject.FindGameObjectsWithTag("Coin");
        questionBlocks = GameObject.FindGameObjectsWithTag("Question Block");
        foreach (GameObject qB in questionBlocks)
        {
            Rigidbody2D qBBody = qB.GetComponent<Rigidbody2D>();
            qBBody.bodyType = RigidbodyType2D.Dynamic;
            Animator qBAnimator = qB.GetComponent<Animator>();
            qBAnimator.SetTrigger("gameRestart");   // qb animation
            qBAnimator.SetBool("collected", false);
        }
        foreach (GameObject coin in coins)
        {
            Animator coinAnimator = coin.GetComponent<Animator>();
            coinAnimator.SetTrigger("gameRestart");   // coin animation
            coinAnimator.SetBool("collected", false);
        }

        alive = true;

        gameCamera.position = new Vector3(0,0,-10);
    }
}