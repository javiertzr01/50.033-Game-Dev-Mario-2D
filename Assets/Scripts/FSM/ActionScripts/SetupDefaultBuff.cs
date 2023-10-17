using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/SetupDefaultBuff")]
public class SetupDefaultBuff : Action
{
    public BoolVariable isInvincible;

    public override void Act(StateController controller)
    {
        BuffStateController b = (BuffStateController) controller;
        if (b.cycleCoroutine != null)
        {
            b.StopRainbow();
        }
        
        // Stop Sound
        b.gameObject.GetComponent<AudioSource>().Stop();

        // Behaviour that makes mario invincible
        isInvincible.value = false;
    }
}
