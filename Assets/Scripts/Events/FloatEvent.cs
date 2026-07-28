using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Float Event", menuName = "Events/Float Event")]
public class FloatEvent : ScriptableObject
{

    public Action<float> gameEvent;

    public void RaiseEvent(float eventValue)
    {
        gameEvent?.Invoke(eventValue);
    }
}