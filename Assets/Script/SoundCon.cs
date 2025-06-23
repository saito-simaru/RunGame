using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCon : MonoBehaviour
{
    public AudioClip jumpClip;
    public AudioClip damageClip;
    public AudioClip starClip;
    public AudioSource audioSource;

    void Awake()
    {

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "jump":
                if (jumpClip != null)
                {
                    Debug.Log("ジャンプ音を再生します！");
                    audioSource.PlayOneShot(jumpClip);
                }
                else
                {
                    Debug.LogWarning("jumpClip が null です！");
                }
                break;
            case "damage":
                audioSource.PlayOneShot(damageClip);
                break;
            case "star":
                audioSource.PlayOneShot(starClip);
                break;
            default:
                Debug.LogWarning("サウンド名が見つかりません: " + soundName);
                break;
        }
    }

}
