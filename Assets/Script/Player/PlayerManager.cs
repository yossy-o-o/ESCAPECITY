using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerManager : MonoBehaviour
{
    private float speed = 5.0f;
    private float jumpForce = 10.0f;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    public LayerMask groundLayers;
    public float groudCheckRadius = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //横方向の入力を取得
        float verticalInput  = Input.GetAxis("Vertical");  //縦方向の入力を取得
        rb.velocity = new Vector3(horizontalInput * speed, rb.velocity.y, verticalInput * speed);
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce,ForceMode.VelocityChange);//AddForceで上方向に力を与える、ForceMode.Impulseは一気に力を与えるやつ
            Debug.Log("判定丸");
        }
    }

    private bool IsGrounded()
    {
        
        //boxCollider.bounds.centerはboxcolliderのX軸の真ん中をとってる、次はyのboxColliderの最小値をとってる、Zも同じように真ん中をとってる
        Vector3 groudCheckPosition = new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.min.y, boxCollider.center.z);//足元の座標

        //Physics.CheckSphereで判定の弾を出して、groudCheckPositionで定義したサイズをgroudCheckRadiusで半径分の判定玉でgroundLayersについているかを判断している
        return Physics.CheckSphere(groudCheckPosition, groudCheckRadius, groundLayers);
    }
    
}
