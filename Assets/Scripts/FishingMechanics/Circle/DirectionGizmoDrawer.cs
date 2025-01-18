using UnityEngine;

public class DirectionGizmoDrawer : MonoBehaviour
{
    [HideInInspector]
    public Vector3 direction;
    public Color circleColor = Color.green;
    public Color lineColor = Color.red;
    
    private float _radius = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = circleColor;
        Gizmos.DrawWireSphere(transform.position, _radius);

        Gizmos.color = lineColor;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}
