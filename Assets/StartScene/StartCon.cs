using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartCon : MonoBehaviour
{
    SoundCon soundCon;

    void Start()
    {
        soundCon = FindObjectOfType<SoundCon>();
    }

    public void ChangeToTargetScene()
    {
        soundCon.PlaySound("tap");
        SceneManager.LoadScene("SampleScene");
    }

}
