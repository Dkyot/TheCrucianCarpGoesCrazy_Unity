using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class MouseDirectionTracker : MonoBehaviour
{
    private Vector2 _currentVelocity;
    private Vector2 _currentDirection;
    private float _magnitude = 0f;

    [SerializeField] private InputActionAsset inputActions;
    private InputAction mouseDeltaAction;

    private float decayRate = 10f;
    private int smoothingFrames = 50;
    private Queue<Vector2> velocityHistory = new Queue<Vector2>();

    [SerializeField] private DirectionGizmoDrawer _gizmoDrawer;

    private void Update()
    {
        UpdateDecayedHistory();
        CalculateAverageVelocity();

        // Gizmo
        if (_gizmoDrawer != null) _gizmoDrawer.direction = new Vector3(_currentDirection.x, _currentDirection.y, 0) * _magnitude;
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();

        velocityHistory.Enqueue(delta);
        if (velocityHistory.Count > smoothingFrames)
        {
            velocityHistory.Dequeue();
        }
        
        _currentVelocity = delta;
        _currentDirection = delta.normalized;
    }

    #region PublicMethods
    public Vector2 GetCurrentDirection()
    {
        return _currentDirection;
    }
    #endregion

    #region AuxiliaryMethods
    private void UpdateDecayedHistory()
    {
        Queue<Vector2> decayedHistory = new Queue<Vector2>();
        foreach (var velocity in velocityHistory)
        {
            var decayedVelocity = Vector2.Lerp(velocity, Vector2.zero, decayRate * Time.deltaTime);
            decayedHistory.Enqueue(decayedVelocity);
        }

        velocityHistory = decayedHistory;
    }

    private void CalculateAverageVelocity()
    {
        if (velocityHistory.Count > 0)
        {
            Vector2 averageVelocity = Vector2.zero;
            foreach (var velocity in velocityHistory)
            {
                averageVelocity += velocity;
            }
            averageVelocity /= velocityHistory.Count;
            
            _currentVelocity = averageVelocity;
            _currentDirection = _currentVelocity.normalized;

            _magnitude = (_currentVelocity.magnitude > 1f) ? 1f: _currentVelocity.magnitude;
        }
    }
    #endregion

    #region EventSubscription
    private void OnEnable()
    {
        // Инициализация Action из InputActionAsset
        var actionMap = inputActions.FindActionMap("MouseControls");
        mouseDeltaAction = actionMap.FindAction("MouseDelta");

        // Включаем и подписываемся на событие
        mouseDeltaAction.Enable();
        mouseDeltaAction.performed += OnMouseMove;
    }

    private void OnDisable()
    {
        // Отписываемся и отключаем Action
        mouseDeltaAction.performed -= OnMouseMove;
        mouseDeltaAction.Disable();
    }
    #endregion
}
