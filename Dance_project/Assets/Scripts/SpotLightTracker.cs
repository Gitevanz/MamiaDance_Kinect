using UnityEngine;
using UnityEngine.Rendering;


public class SpotLightTracker : MonoBehaviour
{
  
    public float posX;
    [Range(-1f, 1f)]
    public float offsetPosX, inMinPosX, inMaxPosX, outMinPosX, outMaxPosX;
    

    public float posY;
    [Range(-1f, 1f)]
    public float offsetPosY, inMinPosY, inMaxPosY, outMinPosY, outMaxPosY;

    public float size;
    public GameObject trackObj;

  private Material material;

    private void Start()
    {
        // Get the material that uses the HDRP Graph Shader
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        posX = (trackObj.transform.localPosition.x + offsetPosX) * -1;
        posX = Mapper(posX, inMinPosX, inMaxPosX, outMinPosX, outMaxPosX);

        posY = (trackObj.transform.localPosition.y + offsetPosY) * -1;
        posY = Mapper(posY, inMinPosY, inMaxPosY, outMinPosY, outMaxPosY);
        // Set the values of the properties in the material
        material.SetFloat("_PosX", posX);
        material.SetFloat("_PosY", posY);
        material.SetFloat("_Size", size);

      
    }
    public float Mapper(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}