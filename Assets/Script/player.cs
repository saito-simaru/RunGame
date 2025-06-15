using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public HPcontroller hpcon;
    private int MaxJumpCount = 2;
    private int jumpCount = 0;
    [SerializeField]
    private int hp = 3;
    [SerializeField]
    private float cooldownTime;
    public float jumpForce = 5f; // ジャンプ力
    private Vector3 playerPosition;
    private RaycastHit2D hit;
    public float rayLength;
    private Rigidbody2D rb;
    private bool isGrounded = false; // 地面に接触しているかどうかのフラグ
    public LayerMask targetLayer;
    private bool isGameOver = false;
    private bool canDetect = true;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerPosition = gameObject.transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("stage"))
        {
            jumpCount = 0;
            anim.SetBool("isJumping", false);
            Debug.Log("JumpReset");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //canDetectがtrueじゃないと実行されない
        if (!canDetect) return;

        if (other.gameObject.CompareTag("obstacles"))
        {
            Debug.Log("障害物に触れた！");
            hp -= 1;
            //障害物との接触判定OFF
            canDetect = false;
            //Invoke（関数名、関数を呼び出すまでの時間）
            Invoke(nameof(ResetDetection), cooldownTime);

            if (hp == 0)
            {
                Debug.Log("dead");
                anim.SetBool("isDead", true);
            }
        }
    }

    private void ResetDetection()
    {
        //障害物との接触判定ON
        canDetect = true;
    }

    void OnJump()
    {
        if (jumpCount < MaxJumpCount && isGameOver == false)
        {
            //ジャンプ数をカウント
            jumpCount++;
            // ジャンプ力を適用
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("isJumping", true);
        }
    }

    public int GetHP()
    {
        return hp;
    }
}