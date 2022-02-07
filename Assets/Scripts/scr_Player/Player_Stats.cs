using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Stats : MonoBehaviour
{
    [Header("Stats")]
    public int lives;
    public int money;
    public int mainThrusterfuel;
    public int leftThrusterfuel;
    public int rightThrusterfuel;

    [Header("Assignables")]
    public TMP_Text txt_Lives;
    public TMP_Text txt_Money;
    public TMP_Text txt_MainThrusterFuel;
    public TMP_Text txt_LeftThrusterFuel;
    public TMP_Text txt_RightThrusterFuel;

    private void Start()
    {
        txt_Lives.text = lives.ToString();
        txt_Money.text = money.ToString();

        txt_MainThrusterFuel.text = mainThrusterfuel.ToString();
        txt_LeftThrusterFuel.text = leftThrusterfuel.ToString();
        txt_RightThrusterFuel.text = rightThrusterfuel.ToString();
    }
}