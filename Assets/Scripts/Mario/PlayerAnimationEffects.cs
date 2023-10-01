using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerAnimationEffects : MonoBehaviour
{
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    public GameManager gameManager;

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
        marioAudio.PlayOneShot(marioDeath);
    }

    void PlayDeathImpulse()
    {
        GetComponentInParent<PlayerMovement>().DeathImpulse();
    }

    void PlayGameOverScene()
    {
        gameManager.GameOver();
    }
}
