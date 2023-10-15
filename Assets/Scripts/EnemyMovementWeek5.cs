using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovementWeek5 : MonoBehaviour
{
    public GameConstants gameConstants;

    // Movement
    public int moveRight = -1;
    private float enemyPatroltime = 2.0f;
    private Vector2 velocity;

    // Position
    private float maxOffset = 5.0f;
    private Vector3 startPosition;

    // Structure
    private Rigidbody2D enemyBody;
    public Animator goombaAnimator;
    private AudioSource goombaAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        goombaAudio = GetComponent<AudioSource>();
        enemyBody = GetComponent<Rigidbody2D>();
        startPosition = retrieveStartPosition();
        transform.localPosition = startPosition;
        ComputeVelocity();
    }

    Vector3 retrieveStartPosition()
    {
        string goombaName = transform.gameObject.name;
        int goombaIndex = int.Parse(goombaName.Substring(goombaName.Length-1));
        return gameConstants.goombaSpawnLocations[SceneManager.GetActiveScene().name][goombaIndex - 1];
    }

    void ChangeDirection()
    {
        moveRight *= -1;
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2(moveRight * maxOffset / enemyPatroltime, 0);   // Velocity = Displacement / Time
    }

    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);    // We use MovePosition because Goombas are Kinematic type Rigidbody. DO NOT just transform.
    }

    public void Die()
    {
        goombaAudio.PlayOneShot(goombaAudio.clip);
        GetComponent<EdgeCollider2D>().enabled = false;
        goombaAnimator.SetBool("onStomped", true);
        moveRight = 0;
        ComputeVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - startPosition.x) < maxOffset)
        {
            Movegoomba();   // Move Goomba
        }
        else
        {
            ChangeDirection();
            Movegoomba();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (((1 << 7) & (1 << collider.gameObject.layer)) > 0)
        {
            ChangeDirection();
        }
    }

    public void GameRestart()
    {
        goombaAnimator.SetTrigger("gameRestart");
        goombaAnimator.SetBool("onStomped", false);
        GetComponent<EdgeCollider2D>().enabled = true;
        transform.localPosition = startPosition;
        moveRight = -1;
        ComputeVelocity();
    }
}
