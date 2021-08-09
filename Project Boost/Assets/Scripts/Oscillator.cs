using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float movementFactor;
    [SerializeField] float period = 5f; // duration of a single cycle

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // Mathf.Epsilon is the tiniest float value recognized.
        // So we compare to this rather than to precise zero,
        //  which leaves room for error.
        // So because period is float, compare it with Epsilon,
        //  not with zero.
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2;  // constant value of 6.283...
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 (so it's cleaner)

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
