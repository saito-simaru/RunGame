using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl_0 : MonoBehaviour
{
    [Header("BackgroundNum 0 -> 3")]
    public int backgroundNum;
    public Sprite[] Layer_Sprites;
    private GameObject[] Layer_Object = new GameObject[4];
    private int max_backgroundNum = 1;
    void Start()
    {
        for (int i = 0; i < Layer_Object.Length; i++){
            Layer_Object[i] = GameObject.Find("Layer_" + i);
        }
    }

    void Update() {
        //for presentation without UIs
        if (Input.GetKeyDown(KeyCode.RightArrow)) NextBG();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) BackBG();
    }

    void ChangeSprite(){
        //一番奥の背景を変更
        //[backgroundNum * 5]は各セットの先頭レイヤーをとってくるため
        //Layer_Object[0].GetComponent<SpriteRenderer>().sprite = Layer_Sprites[backgroundNum*4];
        //それ以外の背景を変更
        for (int i = 0; i < Layer_Object.Length; i++)
        {
            Sprite changeSprite = Layer_Sprites[backgroundNum * 4 + i];
            //Change Layer_1->7
            //親背景の変更
            Layer_Object[i].GetComponent<SpriteRenderer>().sprite = changeSprite;
            //Change "Layer_(*)x" sprites in children of Layer_1->7
            //子背景の変更
            Layer_Object[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = changeSprite;
            Layer_Object[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = changeSprite;
        }
    }

    public void NextBG(){
        backgroundNum = backgroundNum + 1;
        if (backgroundNum > max_backgroundNum) backgroundNum = 0;
        ChangeSprite();
    }
    public void BackBG(){
        backgroundNum = backgroundNum - 1;
        if (backgroundNum < 0) backgroundNum = max_backgroundNum;
        ChangeSprite();
    }
}
