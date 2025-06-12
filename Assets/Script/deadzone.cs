using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadzone : MonoBehaviour
{
    public scoreManagement score;
    void OnTriggerEnter2D()
    {
        Debug.Log("in Deadzone");
        // score.SetResult();
    }
}
