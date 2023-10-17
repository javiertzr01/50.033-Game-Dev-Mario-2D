using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/StartStarman")]
public class StartStarman : Action
{
    public BoolVariable isInvincible;
    public AudioClip invincibleAudio;

    public override void Act(StateController controller)
    {
        // controller.gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController;
        BuffStateController b = (BuffStateController) controller;
        b.Rainbow();
        b.gameObject.GetComponent<AudioSource>().PlayOneShot(invincibleAudio);

        // Behaviour that makes mario invincible
        isInvincible.value = true;
    }
}
