using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartManager : MonoBehaviour
{
    public void Restart()
    {
        // 1. 現在アクティブなシーンの名前を取得する
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 2. そのシーンをロードし直す
        SceneManager.LoadScene(currentSceneName);
    }
}
