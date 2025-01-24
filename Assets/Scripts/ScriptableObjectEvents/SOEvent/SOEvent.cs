using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/SO Event")]
public class SOEvent : ScriptableObject
{
    private readonly List<ISOEventListener> listeners = new List<ISOEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(ISOEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(ISOEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
