using UnityEngine;

public class SmoothDirectionChange : MonoBehaviour
{
    private Vector2 _currentDirection;
    private Vector2 _targetDirection;
    private float _changeSpeed = 2f;

    [SerializeField] private Vector2SOEvent vector2Event;

    public void TriggerVector2Event(Vector2 value)
    {
        vector2Event.Raise(value);
    }

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

        TriggerVector2Event(_currentDirection);//!------
    }

    #region PublicMethods
    public Vector2 GetCurrentDirection()
    {
        return _currentDirection;
    }
    #endregion
}


//TODO случайное значение в диапазоне (чтобы рыба не находилась в центре). диапазон может быть больше 1