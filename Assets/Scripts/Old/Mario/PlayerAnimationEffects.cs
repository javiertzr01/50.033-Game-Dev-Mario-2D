using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerAnimationEffects : MonoBehaviour
{
    public AudioSource marioAudio;
    public AudioSource marioDeath;

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
        GetComponentInParent<PlayerMovement>().DeathImpulse();
    }

    void PlayGameOverScene()
    {
        GameManager.instance.GameOver();
    }
}
