using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Bool Event", menuName = "Events/Bool Event")]
public class BoolEvent : ScriptableObject
{

    public Action<bool> gameEvent;

    public void RaiseEvent(bool eventValue)
    {
        gameEvent?.Invoke(eventValue);
    }
}
