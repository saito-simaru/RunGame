using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCon : MonoBehaviour
{
    public AudioClip jumpClip;
    public AudioClip secondjumpClip;
    public AudioClip damageClip;
    public AudioClip starClip;
    public AudioClip tapClip;
    public AudioClip speedClip;

    public AudioSource audioSource;


    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "jump":
                Debug.Log("ジャンプ音を再生します！");
                audioSource.PlayOneShot(jumpClip);
                break;
            case "secondjump":
                audioSource.PlayOneShot(secondjumpClip);
                break;
            case "damage":
                audioSource.PlayOneShot(damageClip);
                break;
            case "star":
                audioSource.PlayOneShot(starClip);
                break;
            case "tap":
                audioSource.PlayOneShot(tapClip);
                break;
            case "speed":
                audioSource.PlayOneShot(speedClip);
                break;
            default:
                Debug.LogWarning("サウンド名が見つかりません: " + soundName);
                break;
        }
    }

}
