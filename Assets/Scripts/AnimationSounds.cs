using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    public AudioSource marioAudio;

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
}
