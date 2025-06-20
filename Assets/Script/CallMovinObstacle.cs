using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class CallMovinObstacle : MonoBehaviour
{
    public GameObject movinObstacle;
    public GameObject player;
    private float spawnPosx;

    void Start()
    {
        //StartCoroutine(CallmovinObstacleLoop());
    }

    public IEnumerator CallmovinObstacleLoop()
    {
        Debug.Log("お化け開始");
        while (true)
        {
            // ランダムな待ち時間（例：1秒〜3秒）
            float waitTime = Random.Range(5f, 10f);
            yield return new WaitForSeconds(waitTime);
                
            CallmovinObstacle();
        }
    }

    private void CallmovinObstacle()
    {
        // 大体画面端に出現させる
        float spawnPosx = player.transform.position.x + 30;

        // ランダムな値を生成
        float upperY = Random.Range(3f, 6f);
        float lowerY = Random.Range(-1.5f, upperY - 2f); // 上限より下の値にしておく
        float Yspeed = Random.Range(3f, 10f);
        float Xspeed = Random.Range(0.05f, 0.1f);

        // インスタンス化
        GameObject newObstacle = Instantiate(movinObstacle, new Vector3(spawnPosx, lowerY, 0), Quaternion.identity);

        // スクリプト取得＆初期化
        movinObstacle script = newObstacle.GetComponent<movinObstacle>();
        script.Initialize(upperY, lowerY, Yspeed, Xspeed);

    }
}
