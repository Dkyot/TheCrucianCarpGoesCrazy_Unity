using UnityEngine;

public class CalculatingProgressCircle : MonoBehaviour
{
    private float _innerCircleRadius = 0.5f;
    private float _outerCircleRadius = 1.0f;

    private float _progressIncreaseSpeed = 1.0f;
    private float _progressDecreaseSpeed = 0.5f;

    private float _startProgress = 0.0f;
    private float _progress = 0.0f;

    [SerializeField] private VectorDifferenceCalculator _vectorDifference;

    private void Start()
    {
        _progress = _startProgress;
    }

    private void Update()
    {
        UpdateProgress();
    }

    #region PublicMethods
    public float GetCurrentProgress()
    {
        return _progress;
    }
    #endregion

    #region AuxiliaryMethods
    private void UpdateProgress()
    {
        float magnitude = _vectorDifference.GetCurrentMagnitude();
        if (magnitude <= _innerCircleRadius)
        {
            _progress += _progressIncreaseSpeed * Time.deltaTime;
        }
        else if (magnitude <= _outerCircleRadius)
        {
            _progress -= _progressDecreaseSpeed * Time.deltaTime;
        }
        else
        {
            _progress -= _progressDecreaseSpeed * 2 * Time.deltaTime;
        }

        _progress = Mathf.Clamp01(_progress);
    }
    #endregion
}
