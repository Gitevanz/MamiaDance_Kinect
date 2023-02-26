using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounceMultiplyer = 1;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(Vector3.up * bounceMultiplyer,ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
