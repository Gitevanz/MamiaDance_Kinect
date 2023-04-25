using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riseSkull : MonoBehaviour
{
    private int movement = 0;
    private bool hasmoved;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (!hasmoved) hasmoved = true;
    }
    private void Update()
    {
        if (hasmoved && movement < 150)
        {
            transform.Translate(Vector3.up * 0.3f * Time.deltaTime);
            movement++;
        }
    }
}
