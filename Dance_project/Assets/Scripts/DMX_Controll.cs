using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArtDotNet;


public class DMX_Controll : MonoBehaviour
{
    
    float value;
    [Header("Input Value")]
    public string targetName = "_Color";
    [Header("Min Max")]
    public float mapMin;
    public float mapMax;
    [Header("Channel")]
    public int DmxChannel;

    Material material;
    public int materialIndex;

    ArtNetClient dmxClient;
    GameObject dmxObj;
    
    


    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {

        dmxObj = GameObject.Find("DMX");
        dmxClient = (ArtNetClient)dmxObj.GetComponent(typeof(ArtNetClient));

        if (materialIndex == 0)
            material = GetComponent<Renderer>().material;
        else
            material = GetComponent<Renderer>().materials[materialIndex];
        DmxChannel = DmxChannel + 17;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        value = Mapper(dmxClient.DMXdata[DmxChannel],0,255, mapMin, mapMax);
        material.SetFloat(targetName, value);

       

    }

    public float Mapper(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
