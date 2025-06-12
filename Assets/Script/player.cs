using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float jumpForce = 500f; // ジャンプ力
    private Vector3 playerPosition;
    private RaycastHit2D hit;
    public float rayLength;
    private Rigidbody2D rb;
    private bool isGrounded = false; // 地面に接触しているかどうかのフラグ
    public LayerMask targetLayer;
    private bool isGameOver = false;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerPosition = gameObject.transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Raymethod();
    }

    void OnCollisionExit2D(Collision2D other)
    {           
        isGrounded = false;
        
    }
    


    // PlayerInputコンポーネントのイベントで呼び出されるメソッド
    // アクションマップ名 + アクション名 をメソッド名にする（例: OnPlayerJump）
    // PlayerInput の Behaviour が "Invoke Unity Events" の場合、このメソッドが呼び出されます。
    void OnJump()
    {

        if (isGrounded == true)
        {
            
            //Impulseは瞬間的に力を加える
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse); // 上方向に力を加える
            isGrounded = false;
        }

    }


    private void Raymethod()
    {
        playerPosition = gameObject.transform.position;
        hit = Physics2D.Raycast(playerPosition, Vector3.down, rayLength, targetLayer);
        //Debug.DrawRay(playerPosition, Vector3.down * rayLength, Color.red);
        //Debug.Log("プレイヤーの下にオブジェクトがあります: " + hit.collider.gameObject.tag);

        if (hit.collider == null)
        {
            Debug.Log("null");
        }
        else if (hit.collider.gameObject.tag == "stage")
        {
            
            isGrounded = true;
        }

    }

}

