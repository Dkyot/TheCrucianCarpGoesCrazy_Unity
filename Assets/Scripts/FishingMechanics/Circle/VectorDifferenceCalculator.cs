using UnityEngine;

public class VectorDifferenceCalculator : MonoBehaviour
{
    [SerializeField] private MouseDirectionTracker _mouseTracker;
    [SerializeField] private SmoothDirectionChange _smoothChange;

    private float _dampingSpeed = 5f;
    private Vector2 _resultDirection;

    //[SerializeField] private DirectionGizmoDrawer _gizmoDrawer;

    private void Update()
    {
        if (_mouseTracker != null && _smoothChange != null)
        {
            Vector2 mouseDirection = _mouseTracker.GetCurrentDirection();
            Vector2 smoothDirection = _smoothChange.GetCurrentDirection();

            Vector2 targetDirection = smoothDirection + mouseDirection;
            _resultDirection = Vector2.Lerp(_resultDirection, targetDirection, _dampingSpeed * Time.deltaTime);

            // Gizmo
            //if (_gizmoDrawer != null) _gizmoDrawer.direction = new Vector3(_resultDirection.x, _resultDirection.y, 0);
        }
    }

    #region PublicMethods
    public float GetCurrentMagnitude()
    {
        return _resultDirection.magnitude;
    }
    #endregion
}
