using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class info : MonoBehaviour
{
    [SerializeField]
    public Sprite[] sprites;
    private string[] strings = new string[4];
    //public GameObject image;
    
    public TextMeshProUGUI text;
    public Canvas canvas;
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer imageRenderer;
    private int currentIndex;
    private int notActive = -2;
    private int Active = 2;
    void Start()
    {
        canvas.sortingOrder = notActive;
        imageRenderer.sortingOrder = notActive - 1;

        strings[0] = "これがあなた。画面をクリックするとジャンプできます。";
        strings[1] = "これがHPです。HPが0になるとゲームオーバーとなります。";
        strings[2] = "ここにスコアが表示されます。";
        strings[3] = "星をとるとスピードアップ！";

        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        imageRenderer.sprite = sprites[0];
        text.text = strings[0];
        currentIndex = 0;
    }

    public void startInfo()
    {
        canvas.sortingOrder = Active;
        imageRenderer.sortingOrder = Active - 1;
    }

    public void endInfo()
    {
        canvas.sortingOrder = notActive;
        imageRenderer.sortingOrder = notActive - 1;
        imageRenderer.sprite = sprites[0];
        currentIndex = 0;
        text.text = strings[0];
    }

    public void changeNextImage()
    {
        if (currentIndex < 3)
        {
            currentIndex += 1;
            imageRenderer.sprite = sprites[currentIndex];
            text.text = strings[currentIndex];
            Debug.Log(currentIndex);
        }
        else
        {
            endInfo();
        }

    }
    public void changeBackImage()
    {
        if (currentIndex != 0)
        {
            currentIndex -= 1;
            imageRenderer.sprite = sprites[currentIndex];
            text.text = strings[currentIndex];
        }
        
    }

}
