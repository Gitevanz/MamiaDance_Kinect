using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VFXcontroller : MonoBehaviour
{
    public VelocityCounter velocityCounter;

    public ParticleSystem vfx;
    private ParticleSystem.EmissionModule emissionModule;
    public Transform pivotPoint;

    public float minRateOverTime;
    public float emissionRateMultiplyer = 1;

    private void Awake()
    {
        emissionModule = vfx.emission;
        velocityCounter.OnMove += Moved;
    }
    private void Start()
    {
        emissionModule.rateOverTime = minRateOverTime;
    }

    private void OnDestroy()
    {
        velocityCounter.OnMove -= Moved;
    }

    void Moved(Vector3 velocity)
    {
        emissionModule.rateOverTime = Mathf.Max(minRateOverTime, velocity.magnitude * emissionRateMultiplyer);

        //Instantiate(vfx, elbowR.position + new Vector3(0,1,0),Quaternion.identity);
        //Debug.Log(velocityCounter.velocity.magnitude);

    }

}
