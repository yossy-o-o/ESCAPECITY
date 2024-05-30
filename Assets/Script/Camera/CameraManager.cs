using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{   
    [SerializeField] GameObject player;
    private UnityEngine.Vector3 playerPos;//プレイヤーの位置の変数
    private float speed = 500f; //カメラのスピードを取得
    private float mouseInputX;
    private float mouseInputY;

    void Start()
    {
        playerPos = player.transform.position;

        // カメラの初期位置をプレイヤーの位置に合わせる
        transform.position = player.transform.position + new Vector3(0, 1.6f, 0); // プレイヤーの頭の位置にカメラを配置

        // カメラの向きをプレイヤーの向きに合わせる
        transform.rotation = player.transform.rotation;
    }

    void Update()
    {
        //playerPos = player.transform.position;で取得した位置を
        //transform.position += player.transform.position - playerPos;で引いて、その差分をplayerPos = player.transform.position;ここでまた取得している
        //カメラ位置をプレイヤーの位置に合わせる
        //カメラの移動 = 移動後座標-移動前座標　
        transform.position += player.transform.position - playerPos;
        //移動前座標 = 移動後座標
        playerPos = player.transform.position;

        //マウスのX軸とY軸の入力を取得
        mouseInputX = Input.GetAxis("Mouse X");
        mouseInputY = -Input.GetAxis("Mouse Y");

        //カメラを回転させる
        transform.RotateAround(playerPos, UnityEngine.Vector3.up, mouseInputX * Time.deltaTime * speed);
        transform.RotateAround(playerPos, transform.right, mouseInputY * Time.deltaTime * speed);
    }
}
