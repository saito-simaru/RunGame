using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreManagement : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI highScoreText;
    public  GameObject player;
    public createStage setStage;
    public Canvas gameUI;
    public Canvas result;
    public GameObject restartCon;
    private const string HIGHSCORE_KEY = "HighScore"; // 保存するデータのキー
    private int currentScore;
    private int highScore;

    void Start()
    {
        restartCon.SetActive(false);
        // アプリ起動時にハイスコアを読み込む
        highScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0); // キーが見つからない場合は0を返す
        Debug.Log("現在のハイスコア: " + highScore);
        //ゲーム起動時にresultキャンパスを取得してから非表示
        result.enabled = false;
    }

    void Update()
    {
        currentScore = (int)player.transform.position.x;
        //カメラの座標がそのままスコア  F0で小数点なし
        currentScoreText.text = "Score:" + currentScore.ToString("F0");
    }

    //deadzonScriptから呼び出すからpublic
    public void SetResult()
    {
        Debug.Log("startResult");
        SaveScore(currentScore);
        gameUI.enabled = false;  
        result.enabled = true;
        restartCon.SetActive(true);
        displayEndScore();
    }
    void displayEndScore()
    {
        endScoreText.text = "Score:" + currentScore.ToString("F0");
        highScoreText.text = "HighScore:" + highScore.ToString("F0");
    }

    void SaveScore(int newScore)
    {
        if (newScore > highScore)
        {
            highScore = newScore;
            PlayerPrefs.SetInt(HIGHSCORE_KEY, highScore);
            PlayerPrefs.Save(); // データをディスクに書き込む（重要！）
            Debug.Log("新しいハイスコアが保存されました: " + highScore);
        }
    }
}
