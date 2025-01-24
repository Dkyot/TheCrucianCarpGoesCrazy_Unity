using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class MouseDirectionTracker : MonoBehaviour
{
    private Vector2 _currentVelocity;
    private Vector2 _currentDirection;
    private Vector2 _currentDirectionMagnitude;
    private float _magnitude = 0f;

    [SerializeField] private InputActionAsset _inputActions;
    private InputAction _mouseDeltaAction;

    private float _decayRate = 10f;
    private int _smoothingFrames = 50;
    private Queue<Vector2> _velocityHistory = new Queue<Vector2>();

    [SerializeField] private Vector2SOEvent vector2Event;

    public void TriggerVector2Event(Vector2 value)
    {
        vector2Event.Raise(value);
    }

    private void Update()
    {
        UpdateDecayedHistory();
        CalculateAverageVelocity();

        _currentDirectionMagnitude = _currentDirection * _magnitude;
        TriggerVector2Event(_currentDirectionMagnitude);//!------
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();

        _velocityHistory.Enqueue(delta);
        if (_velocityHistory.Count > _smoothingFrames)
        {
            _velocityHistory.Dequeue();
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
        foreach (var velocity in _velocityHistory)
        {
            var decayedVelocity = Vector2.Lerp(velocity, Vector2.zero, _decayRate * Time.deltaTime);
            decayedHistory.Enqueue(decayedVelocity);
        }

        _velocityHistory = decayedHistory;
    }

    private void CalculateAverageVelocity()
    {
        if (_velocityHistory.Count > 0)
        {
            Vector2 averageVelocity = Vector2.zero;
            foreach (var velocity in _velocityHistory)
            {
                averageVelocity += velocity;
            }
            averageVelocity /= _velocityHistory.Count;
            
            _currentVelocity = averageVelocity;
            _currentDirection = _currentVelocity.normalized;

            _magnitude = (_currentVelocity.magnitude > 1f) ? 1f: _currentVelocity.magnitude;
        }
    }
    #endregion

    #region EventSubscription
    private void OnEnable()
    {
        var actionMap = _inputActions.FindActionMap("MouseControls");
        _mouseDeltaAction = actionMap.FindAction("MouseDelta");

        _mouseDeltaAction.Enable();
        _mouseDeltaAction.performed += OnMouseMove;
    }

    private void OnDisable()
    {
        _mouseDeltaAction.performed -= OnMouseMove;
        _mouseDeltaAction.Disable();
    }
    #endregion
}
