using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartManager : MonoBehaviour
{
    private SoundCon soundCon;

    void Start()
    {
        soundCon = FindObjectOfType<SoundCon>();
    }
    public void Restart()
    {
        soundCon.PlaySound("tap");
        // 1. 現在アクティブなシーンの名前を取得する
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 2. そのシーンをロードし直す
        SceneManager.LoadScene(currentSceneName);
    }

    public string sceneName; // 遷移先シーン名をInspectorで指定
    public void ChangeScene()
    {
        soundCon.PlaySound("tap");
        SceneManager.LoadScene(sceneName);
    }
}
