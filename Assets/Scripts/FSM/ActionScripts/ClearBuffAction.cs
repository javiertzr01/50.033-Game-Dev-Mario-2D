using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/ClearBuffAction")]
public class ClearBuffAction : Action
{
    public override void Act(StateController controller)
    {
        BuffStateController b = (BuffStateController) controller;
        b.currentPowerupType = PowerupType.Default;
    }
}
