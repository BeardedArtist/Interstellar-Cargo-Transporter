using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;

    // period will be the variable that helps us define the amount of time to divide for cycles
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // we need to set up a mechanism for measuring time.
        // Time.time measures time that has elapsed.
        // the variable period will be used to help us define the amount of time we want.
        // Therefore, if 10 seconds has passed, and period is set to 2, then cycle will equal 5 cycles.
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // continually growing over time (check above for explanation).
        

        // to get our object to move automatically, we need to set up the radian Tau
        // using const because a const (constant) won't change
        const float tau = Mathf.PI * 2; // constant value of 6.283...
        float rawSinWave = Mathf.Sin(cycles * tau); // multiplying cycles by tau should give us a radian value between -1 and 1

        // now we need to set our movementFactor to automatically cycle between 0 and 1 instead of
        // -1 and 1.
        // to do this we will add rawSinWave + 1f because -1 + 1 = 0 and 1 + 1 = 2 thus this becomes 0 to 2
        // then we will divide by 2 because 0 / 2 = 0 and 2/2 = 1. Thus we get 0 to 1
        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1


        // to get our object movement logic, we need to multiply the movementVector by the movementFactor
        Vector3 offset = movementVector * movementFactor;
        // now we need to define the new position of the object that is the offset from the original position
        transform.position = startingPosition + offset;
    }
}
