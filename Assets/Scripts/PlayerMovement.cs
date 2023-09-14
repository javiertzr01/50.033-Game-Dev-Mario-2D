using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    public float speed = 10;
    public float maxSpeed = 20;
    public float upSpeed = 10;

    // Position
    private bool onGroundState = true;
    private bool faceRightState = true;

    // Structure
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    public JumpOverGoomba jumpOverGoomba;

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
        marioSprite = GetComponent<SpriteRenderer>();
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
        }
        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Collided with goomba!");
                GameOverScoreText.text = scoreText.text;
                GameOverCanvas.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }

    // FixedUpdate is used for Physics Logic
    void FixedUpdate()
    {
    // Control Mario's Movements
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
        marioBody.transform.position = new Vector3(-8.26f, -4.4f, 0.0f);    // Reset Mario Position
        faceRightState = true;  // Reset Sprite Direction
        marioSprite.flipX = false;  // Reset SPrite Direction
        scoreText.text = "SCORE: 0";    // Reset Score
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }

        jumpOverGoomba.score = 0;   // Reset Score
        GameOverCanvas.SetActive(false);
    }
}
