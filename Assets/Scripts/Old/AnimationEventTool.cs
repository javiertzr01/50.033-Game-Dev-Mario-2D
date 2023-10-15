using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTool : MonoBehaviour
{

    public UnityEvent move;

    void Move()
    {
        move.Invoke();
    }
}
