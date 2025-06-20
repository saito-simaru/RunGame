using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movinObstacle : MonoBehaviour
{
    public float upperY = 3f; // Y座標の上限
    public float lowerY = 1f; // Y座標の下限
    public float Yspeed = 2f;  // Y座標の移動速度
    public float Xspeed = 2f;  // X座標の移動速度

    private bool movingUp = true;

    void Update()
    {


        Vector3 position = transform.position;

        position.x -= Xspeed;

        if (movingUp)
        {
            position.y += Yspeed * Time.deltaTime;
            if (position.y >= upperY)
            {
                position.y = upperY;
                movingUp = false;
            }
        }
        else
        {
            position.y -= Yspeed * Time.deltaTime;
            if (position.y <= lowerY)
            {
                position.y = lowerY;
                movingUp = true;
            }
        }

        transform.position = position;
    }
    public void Initialize(float up, float down, float ySpd, float xSpd)
    {
        upperY = up;
        lowerY = down;
        Yspeed = ySpd;
        Xspeed = xSpd;
    }
}
