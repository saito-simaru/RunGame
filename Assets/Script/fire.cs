using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public player plyaer;

    public void changesizeOfFire()
    {
        Vector3 currentSize = gameObject.transform.localScale;
        Vector3 currentPos = gameObject.transform.position;
        gameObject.transform.localScale = new Vector3(currentSize.x + 0.1f, currentSize.y + 0.1f, 0);
        gameObject.transform.position = new Vector3(currentPos.x - 0.5f, 0, 0);
    }
}
