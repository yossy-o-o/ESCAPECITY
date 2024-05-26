using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

/*カメラとプレイヤーが一緒に動くコード*/
public class PlayerManager1 : MonoBehaviour
{
    private Rigidbody rb;
    public float speed ; //プレイヤーの移動速度
    private float inputHorizontal; //横方向
    private float inputVertical;   //縦方向
    private Vector3 cameraForward; //カメラの前方向
    private Vector3 moveForward;   //カメラの移動方向

    [SerializeField] GameObject GameOverPanel;//GameOverPanelを取得


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        //カメラの前方向を計算
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        //移動方向を計算
        moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        //移動速度を適用し、プレイヤーに移動させる
        rb.velocity = moveForward * speed + new Vector3(0, rb.velocity.y, 0);

        //移動方向がゼロでなければ、プレイヤーの向きを移動方向に変更する
        if(moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
    }
}
