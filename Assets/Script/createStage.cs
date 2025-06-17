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
    public int maxFloors;
    public float floorWidth;
    private float floorTopPos;

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
        
        createSkyFloor(floorSprite);

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
    }

    void createObstacles(GameObject instanceobj, Vector2 position, Vector2 size)
    {
        //引数のオブジェクトをインスタンス化
        GameObject obj = Instantiate(instanceobj);

        int choiceFloorOrSkyfloor = Random.Range(0, 3);

        if (choiceFloorOrSkyfloor < 3)
        {
            
        }

        //renderer.color = Color.gray; 
        floor.transform.position = floorPosition;
        floor.transform.localScale = floorScale;

        Stages.Add(obj);
    }



    public void DeleteStage()
    {
        //削除するオブジェクトをリストから除外
        Stages.RemoveAt(0); 
    }
}
