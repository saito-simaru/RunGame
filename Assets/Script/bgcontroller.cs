using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgcontroller : MonoBehaviour
{
    [SerializeField, Header("視覚効果"), Range(0, 1)]
    private float parallaxEffect;

    private GameObject camera;
    private float length;
    private float startPosx;

    void Start()
    {
        camera = Camera.main.gameObject;
        startPosx = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        Parallax();
    }

    private void Parallax()
    {
        float temp = camera.transform.position.x * (1 - parallaxEffect);
        float dist = camera.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPosx + dist, transform.position.y, transform.position.z);

        if (temp > startPosx + length)
        {
            startPosx += length;
        }
    }
}
