using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class background : MonoBehaviour
{
    public SpriteRenderer FillspriteRenderer; // 対象のSpriteRenderer
    private Color FillColor;

    public float duration = 1f;

    [Header("BackgroundNum 0 -> 3")]
    public int backgroundNum;
    [Header("背景枚数")]
    public int max_backgroundNum;
    public Sprite[] Layer_Sprites;
    private GameObject[] Layer_Object = new GameObject[4];

    //private int max_backgroundNum = 1;
    void Start()
    {
        //フィルターの色を保存＆一応アルファ値を０にして代入
        FillColor = FillspriteRenderer.color;
        FillspriteRenderer.color = new Color(FillColor.r, FillColor.g, FillColor.b, 0f);
        //レイヤーの器を配列に入れる
        for (int i = 0; i < Layer_Object.Length; i++)
        {
            Layer_Object[i] = GameObject.Find("Layer_" + i);
        }

    }

    // public void StartChangeScene()
    // {
    //     StartCoroutine(FadeOutAndIn());
    // }


     public IEnumerator FadeOutAndIn()
    {
        float time = 0f;
        //フェードイン
        while (time < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / duration);
            FillspriteRenderer.color = new Color(FillColor.r, FillColor.g, FillColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        // 念のためαを1に
        FillspriteRenderer.color = new Color(FillColor.r, FillColor.g, FillColor.b, 1f);

        ChangeSprite();

        time = 0f;
        // フェードアウト
        while (time < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / duration);
            FillspriteRenderer.color = new Color(FillColor.r, FillColor.g, FillColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        // 念のためαを0に
        FillspriteRenderer.color = new Color(FillColor.r, FillColor.g, FillColor.b, 0f);

    }
    //背景変更
    void ChangeSprite()
    {
        backgroundNum++; 
        if (backgroundNum > max_backgroundNum) backgroundNum = 0;
        for (int i = 0; i < Layer_Object.Length; i++)
        {
            //一つのステージにつき、４枚の画像を使っているからi(ステージの番号)*4
            Sprite changeSprite = Layer_Sprites[backgroundNum * 4 + i];
            //親背景の変更
            Layer_Object[i].GetComponent<SpriteRenderer>().sprite = changeSprite;
            //子背景の変更
            Layer_Object[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = changeSprite;
            Layer_Object[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = changeSprite;
        }
    }

}
