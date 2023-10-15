using UnityEngine;

public class StateController : MonoBehaviour
{
    public State startState;
    public State previousState;
    public State currentState;
    public State remainState;
    public bool transitionStateChanged = false;
    [HideInInspector] public float stateTimeElapsed;

    private bool isActive = true;

    // Start is called before the first frame update
    public virtual void Start()
    {
        OnSetupState(); // Setup when game starts
    }

    public virtual void OnSetupState()
    {
        if (currentState)
            currentState.DoSetupActions(this);
    }

    public virtual void OnExitState()
    {
        // Reset time in this state
        stateTimeElapsed = 0;
        if (currentState)
            currentState.DoExitActions(this);
    }

    public virtual void OnDrawGizmos()      // For visual aid to indicate which state this object is currently at
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(this.transform.position, 1.0f);
        }
    }

    /********************************/
    // Regular methods
    // no action should be done here, strictly for transition

    public void TransitionToState(State nextState)
    {
        if (nextState == remainState)   return;

        // Following methods only happens once if nextState != remainstate
        OnExitState();

        // Transition the states
        previousState = currentState;
        currentState = nextState;
        transitionStateChanged = true;

        OnSetupState(); // Cast entry action if any
    }

    // default method to check if we've been in the state long enough
    // this method assumes that you will call this once per update frame
    // Time.deltaTime: the interval in seconds from the last frame to the current one (Read Only).

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return stateTimeElapsed >= duration;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isActive)  return; // This is different from gameObject active, allow for separate control

        currentState.UpdateState(this);
    }
    /********************************/


}
