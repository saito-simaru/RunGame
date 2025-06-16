using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HPcontroller : MonoBehaviour
{
    [SerializeField, Header("HPアイコン")]
    private GameObject hpicon;
    private int beforeHP;

    public void createHPIcon(int currenthp)
    {
        for (int i = 0; i < currenthp; i++)
        {
            GameObject playerHPobj = Instantiate(hpicon);
            playerHPobj.transform.SetParent(transform);
        }
    }

    public void showHPIcon(int currenthp)
    {
        if (beforeHP == currenthp) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < currenthp);
        }
        beforeHP = currenthp;
    }
}
