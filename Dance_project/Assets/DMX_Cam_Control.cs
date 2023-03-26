using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArtDotNet;


public class DMX_Cam_Control : MonoBehaviour
{
    Camera camera;
   

    [Header("Camera Position")]
    public float camPosX;
    public float camPosY;
    public float camPosZ;

    [Header("Camera Rotation")]
    public float camRotX;
    public float camRotY;
    public float camRotZ;

    [Header("Camera Property")]
    public bool isOrth;
    public float FOV;
    public float OrthSize;

    [Header("DMX Channel")]
    public int DmxChannel;
    ArtNetClient dmxClient;
    GameObject dmxObj;



    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }
    void Awake()
    {

        dmxObj = GameObject.Find("DMX");
        dmxClient = (ArtNetClient)dmxObj.GetComponent(typeof(ArtNetClient));
    }

    // Update is called once per frame
    void Update()
    {
        camPosX = Mapper(dmxClient.DMXdata[DmxChannel], 0, 255, -10, 10);
        camPosY = Mapper(dmxClient.DMXdata[DmxChannel+1], 0, 255, -10, 10);
        camPosZ = Mapper(dmxClient.DMXdata[DmxChannel+2], 0, 255, -10, 10);
       
        camera.transform.localPosition = new Vector3(camPosX, camPosY, camPosZ);

        camRotX = Mapper(dmxClient.DMXdata[DmxChannel+3], 0, 255, -40, 40); 
        camRotY = Mapper(dmxClient.DMXdata[DmxChannel+4], 0, 255, -40, 40); 
        camRotZ = Mapper(dmxClient.DMXdata[DmxChannel+5], 0, 255, -40, 40);

        camera.transform.eulerAngles = new Vector3(camRotX, camRotY, camRotZ);

        //Orth
        float dmxfovchannel = dmxClient.DMXdata[DmxChannel + 6];

        if (dmxfovchannel >= 127) {
            isOrth = true;
        }
        else { 
            isOrth = false; }

        camera.orthographic = isOrth;

        //Props

        camera.fieldOfView = Mapper(dmxClient.DMXdata[DmxChannel + 8], 0, 255, 20, 50);
        camera.orthographicSize = Mapper(dmxClient.DMXdata[DmxChannel + 7], 0, 255, 1, 6);


    }
public float Mapper(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
