using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Stats : MonoBehaviour
{
    [Header("Stats")]
    public int money;

    [Header("Assignables")]
    public TMP_Text txt_Money;

    private void Start()
    {
        txt_Money.text = money.ToString();
    }
}