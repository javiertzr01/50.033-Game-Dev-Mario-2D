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

    // Position
    private bool onGroundState = true;
    private bool faceRightState = true;
    [System.NonSerialized]
    public bool alive = true;   // To prevent recollision with Goomba

    // Structure
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    public JumpOverGoomba jumpOverGoomba;
    public Animator marioAnimator;
    public Transform gameCamera;

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

    // Update is called once per frame
    void Update()
    {
        // Switching sprite direction
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 2.0f)    // If Mario is turning right abruptly
            {
                marioAnimator.SetTrigger("onSkid"); // Update animator
            }
        }
        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -2.0f)   // If Mario is turning left abruptly
            {
                marioAnimator.SetTrigger("onSkid"); // Update animator
            }
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));  // Update animator
    }

    int collisionLayerMask = (1 << 3) | (1 << 8);

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((collisionLayerMask & (1 << collision.GetContact(0).collider.gameObject.layer)) > 0) && !onGroundState)
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);   // Update animator
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
            marioSprite.GetComponent<AnimationEffects>().PlayDeathSound();
            alive = false;
        }
    }

    // FixedUpdate is used for Physics Logic
    void FixedUpdate()
    {
    // Control Mario's Movements
        if(alive)
        {
            // Horizontal Movement
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(moveHorizontal) > 0)  // If there is input
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);  // Sets up the vector
                if (marioBody.velocity.magnitude < maxSpeed)    // Check if Mario's velocity is greater than the max speed
                {
                    marioBody.AddForce( movement * speed ); // Move Mario
                }
            }
            else
            {
                if (Input.GetKeyUp("a") || Input.GetKeyUp("d") || Input.GetKeyUp("left") || Input.GetKeyUp("right"))    marioBody.velocity = Vector2.zero;  // Stops Mario if key is released
            }
            // Vertical Movement
            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);  // Add impulse up
                onGroundState = false;
                marioAnimator.SetBool("onGround", onGroundState);   //Update animator
            }
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
        alive = true;

        gameCamera.position = new Vector3(0,0,-10);
    }
}
