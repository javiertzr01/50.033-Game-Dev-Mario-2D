using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Movement
    private int moveRight = -1;
    private float enemyPatroltime = 2.0f;
    private Vector2 velocity;

    // Position
    // private float originalX;
    private float originalLocalX;
    private float originalLocalY;
    private float maxOffset = 5.0f;
    public Vector3 startPosition;

    // Structure
    private Rigidbody2D enemyBody;
    public Animator goombaAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        originalLocalX = transform.localPosition.x;
        originalLocalY = transform.localPosition.y;   // Get the starting position
        startPosition = new Vector3(originalLocalX, originalLocalY, 0.0f);
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);   // Velocity = Displacement / Time
    }

    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);    // We use MovePosition because Goombas are Kinematic type Rigidbody. DO NOT just transform.
    }

    public void Die()
    {
        GetComponent<EdgeCollider2D>().enabled = false;
        goombaAnimator.SetBool("onStomped", true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalLocalX) < maxOffset)
        {
            Movegoomba();   // Move Goomba
        }
        else
        {
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
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
