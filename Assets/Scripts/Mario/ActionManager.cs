using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public UnityEvent jump;
    public UnityEvent jumpHold;
    public UnityEvent jumpStop;
    public UnityEvent<int> moveCheck;

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpHold.Invoke();
        }
        else if (context.performed)
        {
            jumpStop.Invoke();
        }
        else if (context.canceled)
        {
            jumpStop.Invoke();
        }
    }

    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jump.Invoke();
        }
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;
            moveCheck.Invoke(faceRight);
        }
        if (context.canceled)
        {
            moveCheck.Invoke(0);
        }
    }

    // public void OnClickAction(InputAction.CallbackContext context)
    // {
    //     if (context.started)
    //     {
    //         Debug.Log("Mouse click started");
    //     }
    //     else if (context.performed)
    //     {
    //         Debug.Log("Mouse click performed");
    //     }
    //     else if (context.canceled)
    //     {
    //         Debug.Log("Mouse click cancelled");
    //     }
    // }

    // public void OnPointAction(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         UnityEngine.Vector2 point = context.ReadValue<UnityEngine.Vector2>();
    //         Debug.Log($"Point detected: {point}");
    //     }
    // }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
