using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAnimation : MonoBehaviour
{
    private Animator brickAnimator;
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
        brickAnimator.Play("brick-bounce");
        brickAnimator.SetTrigger("bounce");
    }
}
