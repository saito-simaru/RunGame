using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    private float playerposx;
    private float offsetx;
    Vector3 camPos;
    void Start()
    {
        //カメラとプレイヤーのｘ軸間の距離を計算
        camPos = transform.position;
        playerposx = player.transform.position.x;
        offsetx = camPos.x - playerposx;
    }
    void LateUpdate()
    {
        //プレイヤー（移動後）のｘ座標にオフセット足してカメラの座標に代入
        playerposx = player.transform.position.x;
        camPos.x = playerposx + offsetx;
        transform.position = camPos;
    }
}
