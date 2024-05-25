using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 2f;
    public Waypoints waypoints;
    private int currentPointIndex = 0;

    void Update()
    {
        if (waypoints == null || waypoints.points.Count == 0)
            return;

        Transform targetPoint = waypoints.points[currentPointIndex];

        // Move towards the target point
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

        // Rotate towards the target point
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Check if the car reached the target point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % waypoints.points.Count;
        }
    }
}

