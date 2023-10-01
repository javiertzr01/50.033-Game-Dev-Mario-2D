using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    private GameObject[] questionBlocks;
    private GameObject[] coins;
    private Transform[] originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");
        questionBlocks = GameObject.FindGameObjectsWithTag("Question Block");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameRestart()
    {
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
    }
}
