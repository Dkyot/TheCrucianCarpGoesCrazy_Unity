using UnityEngine;

public class VectorDifferenceCalculator : MonoBehaviour
{
    [SerializeField] private MouseDirectionTracker _mouseTracker;
    [SerializeField] private SmoothDirectionChange _smoothChange;

    private float _dampingSpeed = 5f;
    private Vector2 _resultDirection;

    [SerializeField] private Vector2SOEvent vector2Event;

    public void TriggerVector2Event(Vector2 value)
    {
        vector2Event.Raise(value);
    }

    private void Update()
    {
        if (_mouseTracker != null && _smoothChange != null)
        {
            Vector2 mouseDirection = _mouseTracker.GetCurrentDirection();
            Vector2 smoothDirection = _smoothChange.GetCurrentDirection();

            Vector2 targetDirection = smoothDirection + mouseDirection;
            _resultDirection = Vector2.Lerp(_resultDirection, targetDirection, _dampingSpeed * Time.deltaTime);

            TriggerVector2Event(_resultDirection);//!------
        }
    }

    #region PublicMethods
    public float GetCurrentMagnitude()
    {
        return _resultDirection.magnitude;
    }
    #endregion
}
