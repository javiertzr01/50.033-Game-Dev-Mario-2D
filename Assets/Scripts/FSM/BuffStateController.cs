using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStateController : StateController
{
    public PowerupType currentPowerupType = PowerupType.Default;
    public BuffState nextState = BuffState.Default;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        GameRestart();
    }
    
    public void GameRestart()
    {
        currentPowerupType = PowerupType.Default;   // Clear Powerup
        TransitionToState(startState);
    }
}
