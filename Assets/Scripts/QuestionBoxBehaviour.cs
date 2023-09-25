using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class QuestionBoxBehaviour : MonoBehaviour
{
    private Rigidbody2D qbBody;
    public Animator coinAnimator;
    private Animator qbAnimator;
    // Start is called before the first frame update
    void Start()
    {
        qbBody = GetComponent<Rigidbody2D>();
        qbAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        coinAnimator.SetTrigger("onHitQB");
        qbAnimator.SetBool("collected", true);
    }

    void setRigidBodyStatic()
    {
        qbBody.bodyType = RigidbodyType2D.Static;
    }

}
