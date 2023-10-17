using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStateController : StateController
{
    public BuffState nextState = BuffState.Default;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        GameRestart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GameRestart()
    {
        TransitionToState(startState);
    }
}
