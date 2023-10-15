using System;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/State")]
public class State : ScriptableObject
{
    public Action[] setupActions;
    public Action[] actions;
    public EventAction[] eventTriggeredActions;
    public Action[] exitActions;
    public Transition[] transitions;

    // For visualisation at the Scene
    public Color sceneGizmoColor = Color.grey;

    /********************************/
    /* REGULAR METHODS */
    // These regular methods cannot be overriden
    public void UpdateState(StateController controller)     // Called at every frame update by state controller -> Do stuff and check for possible transitions
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    protected void DoActions(StateController controller)    // Do stuff; Useful for bots/NPCs
    {
        for (int i = 0; i < actions.Length; i++) 
        {
            actions[i].Act(controller);
        }
    }
    public void DoSetupActions(StateController controller)  // Setup
    {
        for (int i = 0; i < setupActions.Length; i++) 
        {
            setupActions[i].Act(controller);
        }
    }
    public void DoExitActions(StateController controller)   // Exit
    {
        for (int i = 0; i < exitActions.Length; i++)
        {
            exitActions[i].Act(controller);
        } 
    }

    public void DoEventTriggeredActions(StateController controller, ActionType type = ActionType.Default)   
    {   // Actions executed when controller wishes it; I.e. button press/collision
        // Cast all actions that matches given type
        foreach (EventAction eventTriggeredAction in eventTriggeredActions)
        {
            if (eventTriggeredAction.type == type)
            {
                eventTriggeredAction.action.Act(controller);
            }
        }
    }

    protected void CheckTransitions(StateController controller)
    {
        controller.transitionStateChanged = false; // Reset
        for (int i = 0; i < transitions.Length; ++i)
        {
            // Check if the previous transition has caused a change. If yes, stop. Let Update() in StateController run again in the next state
            if (controller.transitionStateChanged)
            {
                break;
            }
            bool decisionSucceeded = transitions[i].decision.Decide(controller);
            if (decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
    /********************************/

}
