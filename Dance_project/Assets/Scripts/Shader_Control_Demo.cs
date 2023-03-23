using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shader_Control_Demo : MonoBehaviour
{

    //Material has to have the shader graph with corresponding variables
    public Material material;

    //Public variables for interfacing with QLC
    public float color;
    public float speed;


    private float actualTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Controlling the shader graph variables with the public variables from QLC
        material.SetColor("_1_color",Color.HSVToRGB(color, 1f, 1f));

        //Custom time variable
        actualTime += Time.deltaTime * speed;
        material.SetFloat("_time", actualTime);
    }
}
