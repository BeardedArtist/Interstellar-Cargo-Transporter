using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env_MoneyCollider : MonoBehaviour
{
    [SerializeField] private int money;
    [SerializeField] private float spinSpeed;

    private void Update()
    {
        //rotate object endlessly
        gameObject.transform.eulerAngles += new Vector3(0, spinSpeed, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //add to player money
            other.GetComponent<Player_Stats>().money += money;
            //update player money text
            other.GetComponent<Player_Stats>().txt_Money.text = other.GetComponent<Player_Stats>().money.ToString();

            Debug.Log("Player picked up " + money + " money!");

            Destroy(gameObject);
        }
    }
}