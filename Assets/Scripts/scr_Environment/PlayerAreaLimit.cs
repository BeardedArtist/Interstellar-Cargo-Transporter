using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAreaLimit : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public GameObject Level1;
    public GameObject Player;
    Vector3 Level1Position = new Vector3(0, 2.5f, 0);

    private void Start() 
    {
        timerIsRunning = true;    
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is leaving the area");
            ActivateTimer();
        }
    }


    void ActivateTimer()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                Debug.Log("Turn back! " + timeRemaining);
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("No Time Left");
                timeRemaining = 0;
                timerIsRunning = false;
                Player.transform.position = Level1Position;
            }
        }
    }
}
