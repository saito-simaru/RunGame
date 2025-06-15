using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HPcontroller : MonoBehaviour
{
    [SerializeField, Header("HPアイコン")]
    private GameObject playericon;

    private player player;
    private int beforeHP;

    void Start()
    {
        player = FindObjectOfType<player>();
        beforeHP = player.GetHP();
        createHPIcon();
    }

    private void createHPIcon()
    {
        for (int i = 0; i < player.GetHP(); i++)
        {
            GameObject playerHPobj = Instantiate(playericon);
            playerHPobj.transform.parent = transform;
        }
    }
    public void showHPIcon()
    {
        if (beforeHP == player.GetHP()) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < player.GetHP());
        }
        beforeHP = player.GetHP();
    }
}
