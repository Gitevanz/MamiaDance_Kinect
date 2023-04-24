using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shader_Controller : MonoBehaviour
{
    // Initiate the input that QLC will communicate with
    public List<MaterialData> materialList;
    private MaterialData mat;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < materialList.Count; i++)
        {
            mat = materialList[i];
            mat.material.SetColor("_1_color", mat.color1);
            mat.material.SetColor("_2_color", mat.color2);
            mat.material.SetFloat("_smoothness", mat.smoothness);
            if (mat.images.Count > mat.imageIndex && mat.imageIndex > -1) mat.material.SetTexture("_image", mat.images[mat.imageIndex]);
            mat.material.SetFloat("_control", mat.control);


            // Time keeping for each material
            mat.localtime += mat.speed * Time.deltaTime;
            mat.material.SetFloat("_time", mat.localtime);
        }
    }
    private void OnValidate()
    {
        for (int i = 0; i < materialList.Count; i++)
        {
            mat = materialList[i];
            mat.name = mat.material.name;
        }
    }

    [System.Serializable]
    public class MaterialData
    {
        [HideInInspector] public string name;
        public Material material; // Input for the material to control
        public Color color1; // Primary color control
        public Color color2; // Secondary color control
        [Range(0, 1f)] public float smoothness; // Roughness control
        public float speed; // Change speed of shader's local time
        public int imageIndex; // Choose which image to use in shader
        public List<Texture> images; // Input images
        public float control; // Reserve variable for shader specific controls

        // A time variable is kept for separate shader speed control
        [HideInInspector] public float localtime;
    }
}
