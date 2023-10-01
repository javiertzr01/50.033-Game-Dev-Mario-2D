using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCoinAnimation : MonoBehaviour
{
    private Animator brickAnimator;
    public Animator coinAnimator;
    // Start is called before the first frame update
    void Start()
    {
        brickAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        coinAnimator.SetBool("collected", true);
        brickAnimator.Play("brick-bounce");
        brickAnimator.SetTrigger("bounce");
    }
}
