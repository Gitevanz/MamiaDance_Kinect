using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArtDotNet;


public class DMX_Controll_Color : MonoBehaviour
{

    Color color;
    [Header("Input Value")]
    public string targetName = "_Color";
    
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

        color = new Color(Mapper(dmxClient.DMXdata[DmxChannel],0,255,0,1), Mapper(dmxClient.DMXdata[DmxChannel+1], 0, 255, 0, 1), Mapper(dmxClient.DMXdata[DmxChannel+2], 0, 255, 0, 1));
        material.SetColor(targetName,color);

       

    }

    public float Mapper(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
