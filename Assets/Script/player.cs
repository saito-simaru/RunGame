using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public HPcontroller hpcon;
    public SpriteBlinker blinker;
    public scoreManagement scoreManagement;
    public LayerMask targetLayer;
    private Vector3 playerPosition;
    private Rigidbody2D rb;
    private Animator anim;
    private int MaxJumpCount = 2;
    private int jumpCount = 0;

    [SerializeField]
    private int hp = 3;
    [SerializeField]
    private float cooldownTime;
    [SerializeField,Header("移動速度")]
    private float movespeed = 1f;
    [SerializeField, Header("落下速度")]
    private float maxFallSpeed = 5;
    private float defaultMovespeed;
    public float jumpForce = 5f; // ジャンプ力
    private bool isGrounded = false; // 地面に接触しているかどうかのフラグ
    private bool isGameOver = false;
    private bool canDetect = true;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerPosition = gameObject.transform.position;
        defaultMovespeed = movespeed;
        //別scriptでhpの数だけhpアイコンを作成
        hpcon.createHPIcon(hp);
    }
    void FixedUpdate()
    {
        if (rb.velocity.y < maxFallSpeed)
        {
            Vector3 v = rb.velocity;
            v.y = maxFallSpeed;
            rb.velocity = v;
        }
    }

    void Update()
    {
        //横スクロール
        // X方向に移動、Yはそのまま
        if (isGameOver == false)
        {
            gameObject.transform.position += new Vector3(movespeed * Time.deltaTime, 0f, 0f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("stage")) return;

        GameObject stage = other.gameObject;
        //stage.transform.position.y + (stage.transform.localScale.y / 2) = stageの上辺の高さ
        //0.4 = プレイヤーの下辺より少し上
        float criteriaOfLanding = stage.transform.position.y + (stage.transform.localScale.y / 2);
        //プレイヤーがstageに横から触れたかを判定
        if (criteriaOfLanding < gameObject.transform.position.y)
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
        //障害物に当たった場合
        if (other.gameObject.CompareTag("obstacles") && canDetect == true)
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