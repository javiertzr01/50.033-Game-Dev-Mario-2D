using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/TransformBuff")]
public class TransformBuffDecision : Decision
{
    [System.Serializable]
    public struct StateTransformMap
    {
        public BuffState fromState;
        public PowerupType powerupCollected;
    }

    public StateTransformMap[] map;

    public override bool Decide(StateController controller)
    {
        BuffStateController b = (BuffStateController) controller;

        BuffState toCompareState = EnumExtension.ParseEnum<BuffState>(b.currentState.name);

        for (int i = 0; i < map.Length; i++)
        {
            if (toCompareState == map[i].fromState && b.currentPowerupType == map[i].powerupCollected)
            {
                return true;
            }
        }

        return false;

    }    
}
