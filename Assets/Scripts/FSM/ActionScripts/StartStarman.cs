using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/StartStarman")]
public class StartStarman : Action
{
    // public RuntimeAnimatorController animatorController;
    public BoolVariable isInvincible;

    public override void Act(StateController controller)
    {
        // Change the animator
        // controller.gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController;

        // Play sound

        // Behaviour that makes mario invincible
        isInvincible.value = true;
    }
}
