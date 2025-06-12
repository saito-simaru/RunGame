using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
/// <summary>
/// アクター操作・制御クラス
/// </summary>
public class ActorManager : MonoBehaviour
{
	// オブジェクト・コンポーネント参照
	private Rigidbody2D rigid_body2D;
	private SpriteRenderer spriteRenderer;
 
	// 移動関連変数
	public float xSpeed; // X方向移動速度
	public bool rightFacing; // 向いている方向(true.右向き false:左向き)
	public int MaxJumpCount = 2;
    private int jumpCount = 0;
 
	// Start（オブジェクト有効化時に1度実行）
	void Start()
	{
		// コンポーネント参照取得
		rigid_body2D = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		
		// 変数初期化
		rightFacing = true; // 最初は右向き
	}
 
    // Update（1フレームごとに1度ずつ実行）
    void Update()
	{
		// 左右移動処理
		MoveUpdate ();
		// ジャンプ入力処理
		JumpUpdate ();
	}
	
	/// <summary>
	/// Updateから呼び出される左右移動入力処理
	/// </summary>
	private void MoveUpdate ()
	{
		// X方向移動入力
		if (Input.GetKey (KeyCode.RightArrow))
		{// 右方向の移動入力
			// X方向移動速度をプラスに設定
			xSpeed = 6.0f;
 
			// 右向きフラグon
			rightFacing = true;
 
			// スプライトを通常の向きで表示
			spriteRenderer.flipX = false;
		}
		else if (Input.GetKey (KeyCode.LeftArrow))
		{// 左方向の移動入力
			// X方向移動速度をマイナスに設定
			xSpeed = -6.0f;
 
			// 右向きフラグoff
			rightFacing = false;
 
			// スプライトを左右反転した向きで表示
			spriteRenderer.flipX = true;
		}
		else
		{// 入力なし
			// X方向の移動を停止
			xSpeed = 0.0f;
		}
	}
 
	/// <summary>
	/// Updateから呼び出されるジャンプ入力処理
	/// </summary>
	private void JumpUpdate ()
	{
		// ジャンプ操作
		if (Input.GetKeyDown (KeyCode.Space) && this.jumpCount<MaxJumpCount)
		{// ジャンプ開始
			// ジャンプ力を計算
			float jumpPower = 15.0f;
			// ジャンプ力を適用
			rigid_body2D.velocity = new Vector2 (rigid_body2D.velocity.x, jumpPower);
			//ジャンプ数をカウント
			jumpCount++;
		}
	}
 		    // 床に着地したら、jumpCountを0にする
    private void OnCollisionEnter2D(Collision2D other)
    	{
        if (other.gameObject.CompareTag("Floor"))
    		{
            jumpCount = 0;
			Debug.Log("JumpReset");
        	}
    	}
		
		void OnTriggerEnter2D(Collider2D other)
		{
			if(other.gameObject.CompareTag("Finish"))
			{
				SceneManager.LoadScene("GoalScene");
			}
		}

	// FixedUpdate（一定時間ごとに1度ずつ実行・物理演算用）
	private void FixedUpdate ()
	{
		// 移動速度ベクトルを現在値から取得
		Vector2 velocity = rigid_body2D.velocity;
		// X方向の速度を入力から決定
		velocity.x = xSpeed;
 
		// 計算した移動速度ベクトルをRigidbody2Dに反映
		rigid_body2D.velocity = velocity;
	}
}