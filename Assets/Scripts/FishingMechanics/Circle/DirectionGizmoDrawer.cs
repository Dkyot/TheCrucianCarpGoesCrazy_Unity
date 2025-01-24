using UnityEngine;

public class DirectionGizmoDrawer : MonoBehaviour, IVector2SOEventListener
{
    [HideInInspector]
    public Vector3 direction;
    public Color circleColor = Color.green;
    public Color lineColor = Color.red;
    
    private float _radius = 1f;

    [SerializeField] private Vector2SOEvent vector2Event;

    public void OnEventRaised(Vector2 value)
    {
        direction = new Vector3(value.x, value.y, 0);
    }

    private void OnEnable()
    {
        if (vector2Event != null)
        {
            vector2Event.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        if (vector2Event != null)
        {
            vector2Event.UnregisterListener(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = circleColor;
        Gizmos.DrawWireSphere(transform.position, _radius);

        Gizmos.color = lineColor;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}
