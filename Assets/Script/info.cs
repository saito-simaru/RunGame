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
    public GameObject image;
    
    public TextMeshProUGUI text;
    public Canvas canvas;
    private SpriteRenderer spriteRenderer;
    private int currentIndex;
    void Start()
    {
        strings[0] = "これがあなた";
        strings[1] = "これがHPです。HPが0になるとゲームオーバーとなります。";
        strings[2] = "ここにスコアが表示されます。";

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        currentIndex = 0;
    }

    public void changeNextImage()
    {
        spriteRenderer.sprite = sprites[currentIndex + 1];
        currentIndex += 1;

        if (currentIndex == 4)
        {
            canvas.enabled = false;
        }
    }
    public void changeBackImage()
    {
        if (currentIndex != 0)
        {
            spriteRenderer.sprite = sprites[currentIndex - 1];
            currentIndex -= 1;
        }
        
    }

}
