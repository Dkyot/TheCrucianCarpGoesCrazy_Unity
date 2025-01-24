using UnityEngine;
using UnityEngine.Events;

public class FloatSOEventListener : MonoBehaviour, IFloatSOEventListener
{
    public FloatSOEvent Event;

    public UnityEvent<float> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(float value)
    {
        Response?.Invoke(value);
    }
}
