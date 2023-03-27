using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shader_Controller : MonoBehaviour
{
    // Initiate the input that QLC will communicate with
    public List<MaterialData> materialList;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < materialList.Count; i++)
        {
            materialList[i].material.SetColor("_1_color", materialList[i].color1);
            materialList[i].material.SetColor("_2_color", materialList[i].color2);
            materialList[i].material.SetFloat("_roughness", materialList[i].roughness);
            materialList[i].material.SetFloat("_time", materialList[i].localtime);
            if (materialList[i].images[materialList[i].imageIndex] != null)
            {
                materialList[i].material.SetTexture("_image", materialList[i].images[materialList[i].imageIndex]);
            }
            materialList[i].material.SetFloat("_control", materialList[i].control);


            // Time keeping for each material
            materialList[i].localtime += materialList[i].speed * Time.deltaTime;
        }
    }
    private void OnValidate()
    {
        for (int i = 0; i < materialList.Count; i++)
        {
            materialList[i].name = materialList[i].material.name;
        }
    }

    [System.Serializable]
    public class MaterialData
    {
        [HideInInspector] public string name;
        public Material material; // Input for the material to control
        public Color color1; // Primary color control
        public Color color2; // Secondary color control
        public float roughness; // Roughness control
        public float speed; // Change speed of shader's local time
        public int imageIndex; // Choose which image to use in shader
        public List<Texture> images; // Input images
        public float control; // Reserve variable for shader specific controls

        // A time variable is kept for separate shader speed control
        [HideInInspector] public float localtime;

    }
}
