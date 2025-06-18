using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class createStage : MonoBehaviour
{
    public Sprite floorSprite;  //Inspector にセットする用
    private Vector2 floorPosition = new Vector2(0,-3.5f);//-3.5は固定値
    private Vector3 floorScale = new Vector3(20,3,1);//y,zは固定値
    private List<GameObject> Stages = new List<GameObject>();
    public Camera camera;
    public GameObject floor;
    public GameObject obstacle;
    public GameObject flyinobstacle;
    public GameObject star;
    public int maxFloors;
    public int createObstacleCount = 3;
    public int createFlyinobstacleCount =2;
    public float floorWidth;
    //private float floorTopPos;
    float skyfloorChance = 0.5f; // 5%の確率

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        //最初の床を作成　これがないと配列が空の状態になり、生成アルゴリズムにエラーが出る
        GameObject obj = Instantiate(floor);

        obj.transform.position = floorPosition;
        obj.transform.localScale = floorScale;

        // スプライトを上書き
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = floorSprite;
        }


        Stages.Add(obj);

        while (Stages.Count < maxFloors)
        {
            CreateStage();
        }
    }

    public void CreateStage()
    {
        SetFloorInformation();
        CreateFloor(floorSprite);

        if (Random.value < skyfloorChance)
        {
            createSkyFloor(floorSprite);
        }
        

        // if (isMountain == true)
        // {
        //     createSkyFloor();
        //     createObstacles();
        // }

        // if (isDesert == true)
        // {
        //     createflyinObstacles();
        // }

        // if (isUnderground == true)
        // {
        //     createswayinObstacles();
        // }
        
    }

    int DivideAndRound(float numerator, float denominator)
    {
        float result = (float)numerator / denominator;
        return Mathf.RoundToInt(result);
    }

    void ChangeLevel()
    {
        floorWidth -= 5;
        //floorSprite = 何か;
    }

    void SetFloorInformation()
    {
        //穴の大きさ
        int holeScale = Random.Range(0, 5);
        //穴が小さすぎたら０とする
        if (holeScale == 2 || holeScale == 1)
        {
            holeScale = 0;
        }
        Debug.Log(holeScale);
        //最後に生成された床を取得
        GameObject lastStage = Stages[Stages.Count - 1];
        //直前に生成されたオブジェクトの右端
        float endx = lastStage.transform.position.x + (lastStage.transform.localScale.x / 2);
        //生成する位置
        floorPosition.x = endx + (floorWidth / 2) + holeScale;
    }
    void CreateFloor(Sprite sprite)
    {
        GameObject obj = Instantiate(floor);

        obj.transform.position = floorPosition;
        obj.transform.localScale = floorScale;

        // スプライトを上書き
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = sprite;
        }

        Stages.Add(obj);

        createObstacles(obj,createObstacleCount);
        createflyinObstacles(obj);
    }
    void createSkyFloor(Sprite sprite)
    {
        GameObject obj = Instantiate(floor);
        obj.tag = "stage";

        //床の横幅割る３を空中床の幅にする
        float Width = DivideAndRound(floorWidth, 3f);
        //0.25は固定値
        obj.transform.localScale = new Vector2(Width, 0.25f);

        //生成する空中床が床の両端の間に収まる位置を計算
        float spawnPosx = Random.Range((floorPosition.x - floorWidth / 2) +
        Width / 2, (floorPosition.x + floorWidth / 2) - Width / 2);
        //2.25は固定値
        obj.transform.position = new Vector2(spawnPosx, 2.25f);

        // スプライトを上書き
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = sprite;
        }

        createObstacles(obj,createObstacleCount - 2);
    }

    void createObstacles(GameObject floor, int createCount)
    {
        // 床のスケールと位置
        Transform floorTf = floor.transform;
        Vector3 floorPos = floorTf.position;
        Vector3 floorScale = floorTf.localScale;

        // 横幅（整数）だけx座標を作る（床の中心から見て -width/2 ～ +width/2）
        int floorWidth = Mathf.FloorToInt(floorScale.x);
        List<float> availableXPositions = new List<float>();

        for (int i = -floorWidth / 2; i < floorWidth / 2; i++)
        {
            float x = floorPos.x + i + 0.5f; // 中心に乗るように+0.5
            availableXPositions.Add(x);
        }

        if (availableXPositions.Count == 0)
        {
            Debug.LogWarning("配置可能なx座標が存在しません");
            return;
        }

        // インスタンスのyスケール（仮）を取得する
        //float objectYScale = obstacle.transform.localScale.y;
        float floorYScale = floorScale.y;

        // Y位置計算： 床の中心 + 床の高さ/2 + オブジェクト高さ/2
        float spawnY = floorPos.y + floorYScale / 2f + 0.5f;

        for (int i = 0; i < createCount; i++)
        {
            // ランダムに1つ選択
            int index = Random.Range(0, availableXPositions.Count);
            float selectedX = availableXPositions[index];
            availableXPositions.RemoveAt(index); 
            // 生成
            Vector3 spawnPosition = new Vector3(selectedX, spawnY, 0f);
            Instantiate(obstacle, spawnPosition, Quaternion.identity);   
        }
    }
    void createflyinObstacles(GameObject floor)
    {
        // 床のスケールと位置
        Transform floorTf = floor.transform;
        Vector3 floorPos = floorTf.position;
        Vector3 floorScale = floorTf.localScale;

        // 横幅（整数）だけx座標を作る（床の中心から見て -width/2 ～ +width/2）
        int floorWidth = Mathf.FloorToInt(floorScale.x);
        List<float> availableXPositions = new List<float>();

        for (int i = -floorWidth / 2; i < floorWidth / 2; i++)
        {
            float x = floorPos.x + i + 1f; // 中心に乗るように+0.5
            availableXPositions.Add(x);
        }

        if (availableXPositions.Count == 0)
        {
            Debug.LogWarning("配置可能なx座標が存在しません");
            return;
        }

        // インスタンスのyスケール（仮）を取得する
        //float objectYScale = obstacle.transform.localScale.y;
        float floorYScale = floorScale.y;

        // Y位置計算： 床の中心 + 床の高さ/2 + オブジェクト高さ/2
        float floorTopPos = floorPos.y + floorYScale / 2f + 1f;

        float spawnY = Random.Range(floorTopPos, 3);

        for (int i = 0; i < createFlyinobstacleCount; i++)
        {
            // ランダムに1つ選択
            int index = Random.Range(0, availableXPositions.Count);
            float selectedX = availableXPositions[index];
            availableXPositions.RemoveAt(index);
            // 生成
            Vector3 spawnPosition = new Vector3(selectedX, spawnY, 0f);
            Instantiate(flyinobstacle, spawnPosition, Quaternion.identity);
        }
    }

    void createStar()
    {
        
    }



    public void DeleteStage()
    {
        //削除するオブジェクトをリストから除外
        Destroy(Stages[0]);
        Stages.RemoveAt(0);
    }
}
