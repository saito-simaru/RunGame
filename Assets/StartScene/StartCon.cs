using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartCon : MonoBehaviour
{


    public void ChangeToTargetScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
