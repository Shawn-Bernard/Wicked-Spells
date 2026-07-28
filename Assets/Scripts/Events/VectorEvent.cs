using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector Event", menuName = "Events/Vector Event")]
public class VectorEvent : ScriptableObject
{

    public Action<Vector3> gameEvent;

    public void RaiseEvent(Vector3 eventValue)
    {
        gameEvent?.Invoke(eventValue);
    }
}
