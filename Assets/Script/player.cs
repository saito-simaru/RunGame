using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    public HPcontroller hpcon;
    public SoundCon soundCon;
    public SpriteBlinker blinker;
    public scoreManagement scoreManagement;
    public createStage createStage;
    public CallMovinObstacle callMovinObstacle;
    public ParticleSystem particleSystem; // Inspectorで設定する or GetComponentで取得
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
    [SerializeField, Header("移動速度")]
    private float movespeed = 1f;
    [SerializeField, Header("最高速度")]
    private float maxSpeed = 12;
    [SerializeField, Header("落下速度制限")]
    private float maxFallSpeed = 5;
    private float defaultMovespeed;
    [SerializeField, Header("入力時のジャンプ力")]
    public float jumpForce = 5f; // ジャンプ力
    [SerializeField, Header("入力中のジャンプ力")]
    private float jumpHoldForce = 5f;
    [SerializeField, Header("最大ジャンプ入力時間")]
    private float jumpHoldDuration = 0.2f;
    private bool isJumping;
    private float jumpTimeCounter;

    private bool isGrounded = false; // 地面に接触しているかどうかのフラグ
    private bool isGameOver = false;
    private bool canDetect = true;
    void Start()
    {
        particleSystem.Stop();
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
            //fire.transform.position += new Vector3(movespeed * Time.deltaTime, 0f, 0f);

        }

        if (isJumping == true && jumpTimeCounter > 0)
        {
            //int roopCount = 0;

            rb.velocity = new Vector2(rb.velocity.x, jumpHoldForce);
            jumpTimeCounter -= Time.deltaTime;
            Debug.Log("ジャンプ入力中処理");
            jumpTimeCounter -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("stage") || other.gameObject.CompareTag("floor"))
        {
            GameObject stage = other.gameObject;
            //stage.transform.position.y + (stage.transform.localScale.y / 2) = stageの上辺の高さ
            //0.4 = プレイヤーの下辺より少し上
            float criteriaOfLanding = stage.transform.position.y + (stage.transform.localScale.y / 2);
            //プレイヤーがstageに横から触れたかを判定
            if (criteriaOfLanding < gameObject.transform.position.y)
            {
                jumpCount = 0;
                anim.SetBool("isJumping", false);
            }
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
            if (movespeed < maxSpeed)
            {
                movespeed += 0.5f;
                soundCon.PlaySound("star");
            }
            if (movespeed == maxSpeed)
            {
                particleSystem.Play();
                soundCon.PlaySound("speed");
            }
            else
            {
                particleSystem.Stop();
            }
            //changesizeOfFire();
                Destroy(other.gameObject);
        }

        //canDetect（接触可能フラグ）がtrueじゃないと実行されない        
        //障害物に当たった場合
        if (other.gameObject.CompareTag("obstacles") && canDetect == true)
        {
            Debug.Log("障害物に触れた！");
            soundCon.PlaySound("damage");
            hp -= 1;
            movespeed = defaultMovespeed;
            hpcon.showHPIcon(hp);
            //fire.transform.localScale = new Vector3(0,0,0);

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
        particleSystem.Stop();
        //fire.transform.localScale = new Vector3(0, 0, 0);
        //お化けの生成を止める
        callMovinObstacle.StopMovinObstacle();
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

    // private void changesizeOfFire()
    // {
    //     Vector3 currentSize = fire.transform.localScale;
    //     Vector3 currentPos = fire.transform.position;
    //     fire.transform.localScale = new Vector3(currentSize.x + 0.5f, currentSize.y + 0.5f, 0);
    //     //fire.transform.position = new Vector3(currentPos.x - 0.25f, 0, 0);
    // }
    public void OnJump(InputAction.CallbackContext context)
    {
        // ボタンが押された瞬間
        if (context.started)
        {
            
            if (jumpCount < MaxJumpCount && !isGameOver)
            {
                if(jumpCount == 0)  soundCon.PlaySound("jump");
                else  soundCon.PlaySound("secondjump");
                jumpCount++;
                isJumping = true;
                jumpTimeCounter = jumpHoldDuration;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                anim.SetBool("isJumping", true);
            }
        }

        // // ボタンを押し続けている間（フレーム毎）
        // if (context.performed && isJumping)
        // {
        //     int roopCount = 0;
        //     while (jumpTimeCounter > 0 && isJumping)
        //     {
        //         roopCount++;
        //         rb.velocity = new Vector2(rb.velocity.x, jumpHoldForce);
        //         jumpTimeCounter -= Time.deltaTime;
        //         if (roopCount > 60)
        //         {
        //             break;
        //         }
        //     }
        //     Debug.Log("ジャンプ入力中処理" + roopCount);

        // }

        // ボタンを離した瞬間
        if (context.canceled)
        {
            
            isJumping = false;
        }
    }
    
}