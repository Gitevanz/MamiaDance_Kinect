using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionEnter : MonoBehaviour
{

    public Transform spawnPoint;
    public ParticleSystem ps;


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collided with " + other.gameObject.name);

        if (other.gameObject.tag == "Ground")
        {
            ps.Play();
            //Instantiate(stepVFX, spawnPoint.position, Quaternion.identity);
        }

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("collided with " + collision.gameObject.name);

    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        Instantiate(stepVFX, spawnPoint.position, Quaternion.identity);
    //    }
    //}
}
