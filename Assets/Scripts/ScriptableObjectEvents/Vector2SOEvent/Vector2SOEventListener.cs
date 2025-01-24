using UnityEngine;
using UnityEngine.Events;

public class Vector2SOEventListener : MonoBehaviour, IVector2SOEventListener
{
    public Vector2SOEvent Event;

    public UnityEvent<Vector2> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Vector2 value)
    {
        Response?.Invoke(value);
    }
}
