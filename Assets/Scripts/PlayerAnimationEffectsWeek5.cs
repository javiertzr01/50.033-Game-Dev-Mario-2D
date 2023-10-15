using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEffectsWeek5 : MonoBehaviour
{
    public AudioSource marioAudio;
    public AudioSource marioDeath;
    public UnityEvent gameOver;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    public void PlayDeathSound()
    {
        marioDeath.PlayOneShot(marioDeath.clip);
    }

    void PlayDeathImpulse()
    {
        GetComponentInParent<PlayerMovementWeek5>().DeathImpulse();
    }

    void PlayGameOverScene()
    {
        gameOver.Invoke();
    }
}
