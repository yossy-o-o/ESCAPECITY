using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 2f;
    public Waypoints waypoints;
    private int currentPointIndex = 0;

    void Update()
    {
        //Waypointが条件を満たしていない場合return
        if (waypoints == null || waypoints.points.Count == 0)
        {
            return;
        }
        
        //Waypointの数によってcurrentPointIndex(リスト)を更新
        Transform targetPoint = waypoints.points[currentPointIndex];

    //車がwaypointまで移動する処理
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

    //車の向きを変える処理
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // Slerpを使って滑らかに回転させる(車の回転数値,WayPointまでの回転数値,回るためのスピード)
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);


        //目標地点に到達したかをチェック
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // 次の目標地点のインデックスを更新する
            // currentPointIndexを1増やし、リスト内をループさせる
            currentPointIndex = (currentPointIndex + 1) % waypoints.points.Count;
        }
    }
}

