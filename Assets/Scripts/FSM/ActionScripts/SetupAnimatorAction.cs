using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/SetupAnimator")]
public class SetupAnimatorAction : Action
{
    public RuntimeAnimatorController animatorController;
    public AudioClip animationAudio;

    public override void Act(StateController controller)
    {
        controller.gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController;
        if(animationAudio != null)
        {
            controller.gameObject.GetComponent<AudioSource>().PlayOneShot(animationAudio);
        }
        
    }
}
