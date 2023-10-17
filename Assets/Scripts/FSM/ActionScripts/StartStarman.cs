using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/StartStarman")]
public class StartStarman : Action
{
    public RuntimeAnimatorController animatorController;
    public override void Act(StateController controller)
    {
        // Change the animator
        // controller.gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController;

        // Behaviour that makes mario invincible
    }
}
