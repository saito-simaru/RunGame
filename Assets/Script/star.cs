using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{
    float noise;
    public float noisevalue;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            noise = Mathf.PerlinNoise(noisevalue - 0.1f, noisevalue);
            Debug.Log(noise);
        }
        
    }
}
