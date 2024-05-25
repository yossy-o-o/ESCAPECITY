using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public List<Transform> points;

    private void OnDrawGizmos()
    {
        if (points == null || points.Count < 2)
            return;

        //Waypointを走るオブジェクトが初期地点に戻った際にループさせないために-1している
        for (int i = 0; i < points.Count - 1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }
    }
}

