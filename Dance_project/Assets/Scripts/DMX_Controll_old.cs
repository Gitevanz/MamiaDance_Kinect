using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArtDotNet;


public class DMX_Controll_old : MonoBehaviour
{
    
    public float vSpeed;
    public float fade;
    public float noise;
    public Material mat;
    
    public ArtNetClient dmxCliet;
    public int DmxChannel;
    public Color mycolor;


    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vSpeed = Mapper(dmxCliet.DMXdata[DmxChannel],0,255,0,10);
        mat.SetFloat("_Speed", vSpeed);

        Color newColor = new Color(Mapper(dmxCliet.DMXdata[DmxChannel + 1], 0, 255, 0, 1), Mapper(dmxCliet.DMXdata[DmxChannel + 2], 0, 255, 0, 1), Mapper(dmxCliet.DMXdata[DmxChannel + 3], 0, 255, 0, 1),1f);
        mat.SetColor("_Color", newColor);
        mycolor = newColor;

        fade = Mapper(dmxCliet.DMXdata[DmxChannel + 4], 0, 255, -1, 1);
        mat.SetFloat("_Fade", fade);

        noise = Mapper(dmxCliet.DMXdata[DmxChannel + 5], 0, 255, -10, 10);
        mat.SetFloat("_Noise", noise);

    }

    public float Mapper(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
