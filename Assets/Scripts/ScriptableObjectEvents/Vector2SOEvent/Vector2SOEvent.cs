using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Vector2 SO Event")]
public class Vector2SOEvent : ScriptableObject
{
    private readonly List<IVector2SOEventListener> listeners = new List<IVector2SOEventListener>();

    public void Raise(Vector2 value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(value);
        }
    }

    public void RegisterListener(IVector2SOEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(IVector2SOEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
