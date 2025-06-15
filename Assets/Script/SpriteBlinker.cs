using UnityEngine;
using System.Collections;

public class SpriteBlinker : MonoBehaviour
{
    public float blinkInterval;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    

    public void StartBlinking(float duration)
    {
        if (!isBlinking)
        {
            Debug.Log("点滅開始");
            StartCoroutine(BlinkCoroutine(duration));
        }
    }

    private IEnumerator BlinkCoroutine(float duration)
    {
        isBlinking = true;
        float time = 0f;
        while (time < duration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            time += blinkInterval;
        }
        spriteRenderer.enabled = true; // 最後に表示状態に戻す
        isBlinking = false;
        Debug.Log("点滅終了");


    }
}
