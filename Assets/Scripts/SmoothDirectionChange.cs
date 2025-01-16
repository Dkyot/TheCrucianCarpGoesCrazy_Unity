using UnityEngine;

public class SmoothDirectionChange : MonoBehaviour
{
    private Vector2 _currentDirection;
    private Vector2 _targetDirection;
    private float _changeSpeed = 2f;

    [SerializeField] private DirectionGizmoDrawer _gizmoDrawer;

    private void Start()
    {
        _targetDirection = _currentDirection = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        _currentDirection = Vector2.Lerp(_currentDirection, _targetDirection, _changeSpeed * Time.deltaTime);

        if (Random.Range(0f, 1f) < 0.01f)
        {
            _targetDirection = Random.insideUnitCircle.normalized;
        }

        // Gizmo
        if (_gizmoDrawer != null) _gizmoDrawer.direction = new Vector3(_currentDirection.x, _currentDirection.y, 0);
    }

    #region PublicMethods
    public Vector2 GetCurrentDirection()
    {
        return _currentDirection;
    }
    #endregion
}
