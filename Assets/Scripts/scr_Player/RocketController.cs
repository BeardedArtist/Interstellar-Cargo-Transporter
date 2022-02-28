using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    // Variables for movement speeds
    [SerializeField] float MainThruster = 1000f;
    [SerializeField] float RotateThruster = 100f;
    [SerializeField] private int mainThrusterfuel = 1000;
    [SerializeField] private int leftThrusterfuel = 1000;
    [SerializeField] private int rightThrusterfuel = 1000;

    // TEST
    float degreesPerSecond = 100f;
    // TEST

    // Varibles for audio clips (LATER)

    // Variables for particle effects (LATER)

    // Varibles for caching
    Rigidbody rb;
    Player_Stats player_Stats;
    RocketController rocketController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player_Stats = GetComponent<Player_Stats>();
        rocketController = GetComponent<RocketController>();

        player_Stats.mainThrusterfuel = mainThrusterfuel;
        player_Stats.txt_MainThrusterFuel.text = player_Stats.mainThrusterfuel.ToString();

        player_Stats.leftThrusterfuel = leftThrusterfuel;
        player_Stats.txt_LeftThrusterFuel.text = player_Stats.leftThrusterfuel.ToString();

        player_Stats.rightThrusterfuel = rightThrusterfuel;
        player_Stats.txt_RightThrusterFuel.text = player_Stats.rightThrusterfuel.ToString();
    }

    private void FixedUpdate() 
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartBoosting();

            if (mainThrusterfuel >= 1)
            {
                DepleteMainThrustFuel();
            }
            else
            {
                player_Stats.txt_MainThrusterFuel.text = "NO FUEL!";
                StopBoosting();
            }
        }
        
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            StopBoosting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (leftThrusterfuel >= 1)
            {
                RotateLeft();
                DepleteLeftThrustFuel();
            }
            else
            {
                player_Stats.txt_LeftThrusterFuel.text = "NO FUEL";
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            if (rightThrusterfuel >= 1)
            {
                RotateRight();
                DepleteRightThrustFuel();
            }
            else
            {
                player_Stats.txt_RightThrusterFuel.text = "NO FUEL";
            }
        }

        else if (Input.GetKey(KeyCode.Q))
        {
            if (rightThrusterfuel >= 1)
            {
                ApplyRoll(degreesPerSecond);
                DepleteLeftThrustFuel();
            }
            else
            {
                player_Stats.txt_LeftThrusterFuel.text = "NO FUEL";
            }
        }

        else if (Input.GetKey(KeyCode.E))
        {
            if (rightThrusterfuel >= 1)
            {
                ApplyRoll(-degreesPerSecond);
                DepleteRightThrustFuel();
            }
            else
            {
                player_Stats.txt_RightThrusterFuel.text = "NO FUEL";
            }
        }

        else if (Input.GetKey(KeyCode.W))
        {
            RotateForward();
        }

        else if (Input.GetKey(KeyCode.S))
        {
            RotateBackward();
        }

        else
        {
            StopRotating();
        }
    }

    void StartBoosting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Adding relative force upword so player can move relative to the object.
            rb.AddRelativeForce(Vector3.up * MainThruster * Time.deltaTime);
        }
    }

    void StopBoosting()
    {
        // TODO: add functionality to stop audio and particle effects
        Debug.Log("Stopped Boosting");
    }

    void RotateLeft()
    {
        ApplyRotation(RotateThruster);
    }

    void RotateRight()
    {
        ApplyRotation(-RotateThruster);
    }

    void RotateForward()
    {
        ApplyFrontalRotation(-RotateThruster);
    }

    void RotateBackward()
    {
        ApplyFrontalRotation(RotateThruster);
    }

    void StopRotating()
    {
        // TODO: Stop particles and audio
    }

    // TEST
    void ApplyRoll(float degreesPerSecond)
    {
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }
    // TEST

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate. 
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over.
    }

    void ApplyFrontalRotation (float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.left * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void DepleteMainThrustFuel()
    {

        player_Stats.mainThrusterfuel = mainThrusterfuel--;
        player_Stats.txt_MainThrusterFuel.text = player_Stats.mainThrusterfuel.ToString();
    }

    void DepleteLeftThrustFuel()
    {
        player_Stats.leftThrusterfuel = leftThrusterfuel--;
        player_Stats.txt_LeftThrusterFuel.text = player_Stats.leftThrusterfuel.ToString();
    }

    void DepleteRightThrustFuel()
    {
        player_Stats.rightThrusterfuel = rightThrusterfuel--;
        player_Stats.txt_RightThrusterFuel.text = player_Stats.rightThrusterfuel.ToString();
    }

}
