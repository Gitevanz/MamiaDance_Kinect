using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArtDotNet;
using UnityEngine.VFX;


public class DMX_Controll_VFX : MonoBehaviour
{
    [Header("Visual Effect")]
    public VisualEffect effect;
    float value;
    [Header("Input Name")]
    public string targetName = "_Value";
    [Header("Min Max")]
    public float mapMin;
    public float mapMax;
    [Header("Channel for Value")]
    public int DmxChannel;
    [Header("Channel for Value Color")]
    public bool useColor;
    public string targetNameColor = "_Color";
    public int DmxChannelColor;


    Vector4 mycolor;

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

        
        DmxChannel = DmxChannel + 17;
        DmxChannelColor = DmxChannelColor + 17;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        value = Mapper(dmxClient.DMXdata[DmxChannel],0,255, mapMin, mapMax);
        effect.SetFloat(targetName, value);

        if(useColor is true)
        {
            mycolor = new Color(Mapper(dmxClient.DMXdata[DmxChannelColor], 0, 255, 0, 1), Mapper(dmxClient.DMXdata[DmxChannelColor+1], 0, 255, 0, 1), Mapper(dmxClient.DMXdata[DmxChannelColor+2], 0, 255, 0, 1));
            effect.SetVector4(targetNameColor, mycolor);
        }
       

    }

    public float Mapper(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
