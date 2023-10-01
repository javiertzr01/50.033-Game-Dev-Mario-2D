using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpOverGoomba : MonoBehaviour
{
    // Player Position
    private bool onGroundState;
    
    // Enemy Position
    // public Transform enemyLocation;
    public GameObject enemies;

    // ScoreState
    private bool countScoreState = false;

    // Structures
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    // Game Manager
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Physics Computation
    void FixedUpdate()
    {
        // Mario Jumps
        if (Input.GetKeyDown("space") && onGroundCheck())
        {
            onGroundState = false;
            countScoreState = true;
        }

        // When Jumping, and Goomba is near Mario and we haven't registered the score
        if (!onGroundState && countScoreState)
        {
            if (checkJumpOverEnemy())
            {
                countScoreState = false;
                gameManager.IncreaseScore(1);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    private bool onGroundCheck()
    {
        
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            Debug.Log("On Ground");
            return true;
        }
        else
        {
            Debug.Log("Not On Ground");
            return false;
        }
    }

    // Helper Gizmo
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    bool checkJumpOverEnemy()
    {
        foreach (Transform eachChild in enemies.transform)
        {
            if (Mathf.Abs(transform.position.x - eachChild.position.x) < 0.5f) return true;
        }
        return false;
    }
}
