using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    // Variables for movement speeds
    [SerializeField] float MainThruster = 1000f;
    [SerializeField] float RotateThruster = 160f;

    // Varibles for audio clips (LATER)

    // Variables for particle effects (LATER)

    // Varibles for caching
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
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

}
