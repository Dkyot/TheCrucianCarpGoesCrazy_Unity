using UnityEngine;

public class VectorDifferenceCalculator : MonoBehaviour
{
    [SerializeField] private MouseDirectionTracker mouseTracker;
    [SerializeField] private SmoothDirectionChange smoothChange;

    [SerializeField] private float dampingSpeed = 5f;
    private Vector2 _resultDirection;

    [SerializeField] private DirectionGizmoDrawer _gizmoDrawer;

    private void Update()
    {
        if (mouseTracker != null && smoothChange != null)
        {
            Vector2 mouseDirection = mouseTracker.GetCurrentDirection();
            Vector2 smoothDirection = smoothChange.GetCurrentDirection();

            Vector2 targetDirection = smoothDirection + mouseDirection;
            _resultDirection = Vector2.Lerp(_resultDirection, targetDirection, dampingSpeed * Time.deltaTime);

            // Gizmo
            if (_gizmoDrawer != null) _gizmoDrawer.direction = new Vector3(_resultDirection.x, _resultDirection.y, 0);
        }
    }
}
