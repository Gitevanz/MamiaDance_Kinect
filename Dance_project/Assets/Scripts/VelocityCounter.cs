using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VelocityCounter : MonoBehaviour
{

    public Vector3 velocity;
    private Vector3 previousPosition;

    public float miniumMovementTolerance;
    public event Action<Vector3> OnMove;

    private void Start()
    {
        previousPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        velocity = transform.position - previousPosition;
        if (velocity.magnitude > miniumMovementTolerance)
        {
            OnMove?.Invoke(velocity);
        }
        previousPosition = transform.position;
    }

}
