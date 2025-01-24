using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Float SO Event")]
public class FloatSOEvent : ScriptableObject
{
    private readonly List<IFloatSOEventListener> listeners = new List<IFloatSOEventListener>();

    public void Raise(float value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(value);
        }
    }

    public void RegisterListener(IFloatSOEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(IFloatSOEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
