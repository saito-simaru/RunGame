using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public HPcontroller hpcon;
    public SpriteBlinker blinker;
    public scoreManagement scoreManagement;
    private int MaxJumpCount = 2;
    private int jumpCount = 0;

    [SerializeField]
    private int hp = 3;
    [SerializeField]
    private float cooldownTime;
    public float movespeed = 1f;
    private float defaultMovespeed;
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
    private Vector3 playerpos;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerPosition = gameObject.transform.position;
        defaultMovespeed = movespeed;
        //別scriptでhpの数だけhpアイコンを作成
        hpcon.createHPIcon(hp);
    }

    void Update()
    {
        //横スクロール
        //rb.velocity = new Vector2(movespeed * Time.deltaTime, rb.velocity.y); // X方向に移動、Yはそのまま
        if (isGameOver == false)
        {
            gameObject.transform.position += new Vector3(movespeed * Time.deltaTime, 0f, 0f);
        }
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
        //gameoverフラグが立ってたら実行されない
        if (isGameOver) return;

        //奈落へ落ちた場合
        if (other.gameObject.CompareTag("deadobj"))
        {
            GameOver();
        }
        //スターに触れた場合
        if (other.gameObject.CompareTag("star"))
        {
            movespeed += 0.5f;
            Destroy(other.gameObject);
        }

        //canDetect（接触可能フラグ）がtrueじゃないと実行されない
        if (!canDetect) return;
        
        //障害物に当たった場合
        if (other.gameObject.CompareTag("obstacles"))
        {
            Debug.Log("障害物に触れた！");
            hp -= 1;
            movespeed = defaultMovespeed;
            hpcon.showHPIcon(hp);

            if (hp == 0)
            {
                GameOver();
            }
            else if (isGameOver == false)
            {
                //障害物との接触判定OFF
                canDetect = false;
                //Invoke（関数名、関数を呼び出すまでの時間）
                Invoke(nameof(ResetDetection), cooldownTime);
                //プレイヤーの点滅処理を別scriptで行う
                blinker.StartBlinking(cooldownTime);
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("dead");

        hp = 0;
        hpcon.showHPIcon(hp);
        isGameOver = true;
        ///rb.AddForce(Vector3.up * 5f, ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, 25);
        anim.SetBool("isDead", true);
        scoreManagement.SetResult();
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
}