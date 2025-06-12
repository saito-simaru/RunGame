using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createStage : MonoBehaviour
{
    public Sprite groundSprite;  //Inspector にセットする用
    private Vector2 createPosition = new Vector2(0,-3.5f);//-3.5は固定値
    private Vector3 createScale = new Vector3(0,3,1);//y,zは固定値
    private List<GameObject> Stages = new List<GameObject>();
    public Camera camera;
    public float moveSpeed = 0f;
    public float interval = 60f;
    public int RandomScaleWidth;
    public int maxObjects;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        // コルーチンを開始
        StartCoroutine(ExecutePeriodically());

        //最初だけ特別にサイズを指定して生成
        GameObject square = new GameObject("ground");
        SpriteRenderer renderer = square.AddComponent<SpriteRenderer>();
        Rigidbody2D rb = square.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static; // 動かない地面用
        square.tag = "stage";

        //inspecter上でstageを選択してる
        square.layer = LayerMask.NameToLayer("stage");


        renderer.sprite = groundSprite;

        // 例: 位置・色・サイズの設定
        renderer.color = Color.gray;
        square.transform.position = new Vector2(0,-3.5f);
        square.transform.localScale = new Vector3(30,3,1);

        BoxCollider2D collider = square.AddComponent<BoxCollider2D>();


        Stages.Add(square);
    }
    IEnumerator ExecutePeriodically()
    {
        while (true) // 無限ループで繰り返し実行
        {
            // 指定した時間だけ待機
            yield return new WaitForSeconds(interval);
            ChangeLevel();

            // ここに60秒ごとに実行したい処理を書く

            // 例: 特定の条件が満たされたらコルーチンを停止する
            // if (GameManager.Instance.IsGameOver)
            // {
            //     yield break; // コルーチンを終了
            // }
        }
    }

    void Update()
    {

        if (Stages.Count < maxObjects)
        {
            SetPositionAndScale();
            CreateStage();
        }
        else
        {
            DeleteStage();
        }

        //横スクロール
        camera.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
    }

    void ChangeLevel()
    {
        RandomScaleWidth -= 5;
    }

    void SetPositionAndScale()
    {
        int stageLength = Random.Range(RandomScaleWidth - 4, RandomScaleWidth); // 1以上11未満 → 1〜10
        createScale.x = stageLength;
        //穴の大きさ
        int holeScale = Random.Range(0, 5);
        //穴が小さすぎたら０とする
        if (holeScale == 2 || holeScale == 1)
        {
            holeScale = 3;
        }

        GameObject lastStage = Stages[Stages.Count - 1];
        //直前に生成されたオブジェクトの端
        float endx = lastStage.transform.position.x + (lastStage.transform.localScale.x / 2);
        //生成する位置
        createPosition.x = endx + (stageLength / 2) + holeScale;
    }


    void CreateStage()
    {
        GameObject square = new GameObject("ground");
        SpriteRenderer renderer = square.AddComponent<SpriteRenderer>();
        Rigidbody2D rb = square.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static; // 動かない地面用
        renderer.sprite = groundSprite;
        square.tag = "stage";
        

        square.layer = LayerMask.NameToLayer("stage");


        // 例: 位置・色・サイズの設定
        renderer.color = Color.gray;
        square.transform.position = createPosition;
        square.transform.localScale = createScale;

        BoxCollider2D collider = square.AddComponent<BoxCollider2D>();

        
        Stages.Add(square);
    }

    void DeleteStage()
    {
        //画面から消えたら
        if (Stages[0].transform.position.x + (Stages[0].transform.localScale.x / 2) < camera.transform.position.x - 15)
        {
            Destroy(Stages[0]);
            Stages.RemoveAt(0); 
        }

    }

}
