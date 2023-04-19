using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArtDotNet;


public class DMX_Controll : MonoBehaviour
{


    public List<ControllerData> controls;

    public ArtNetClient dmxClient;

    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    { 
        foreach (var control in controls)
        {
            foreach (var target in control.targets)
            {
                target.value = Mapper(dmxClient.DMXdata[target.correctedchannel], 0, 255, target.minimumvalue, target.maximumvalue);
            }
        }
    }

    // When the inspector is changed, the target object is checked for changes and all target controls are updated
    private void OnValidate()
    {
        foreach (var control in controls)
        {
            if (control.targetObject.name == "Particle Manager")
            {
                /*foreach (var list in controls.)
                {

                }*/
            }
            if (control.targetObject.name == "Shader Manager")
            {
                for (int i = 0; i < Mathf.Abs(control.targetObject.GetComponent<Shader_Controller>().materialList.Count - control.targets.Count); i++)
                {
                    if (control.targets.Count < control.targetObject.GetComponent<Shader_Controller>().materialList.Count)
                    {
                        control.targets.Add();
                    }
                }
            }
        }
    }

    public float Mapper(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    // Update all values to corresponding fields in the manager
    /*public void UpdateValues()
    {
        foreach (var control in controls)
        {
            if (control.targetObject.name == "Particle Manager")
            {
                foreach (var list in controls.)
                {

                }
            }
            if (control.targetObject.name == "Shader Manager")
            {
                foreach (var targetProperties in control.targetObject.GetComponent<Shader_Controller>().materialList)
                {
                    targetProperties.GetType().GetField("smoothness").SetValue(targetProperties, control.targets[0].value);
                }
            }
        }
    }*/

    // Initializing data types
    // Made public in 'controls' list. ControllerData takes 'Particle manager' or 'Shader manager' as input in 'targetObjects'

    [System.Serializable]
    public class ControllerData {
        public GameObject targetObject;
        public List<TargetFields> targets;

        // TargetFields initializes the values of the target manager and is made public in 'targets' list
        // DMX channel for every controllable value is also defined in TargetFields as 'DmxChannel'
        [System.Serializable]
        public class TargetFields
        {
            public int DmxChannel;
            public float value;

            //public List<float> valuelist;

            [Header("Mapping")]
            public float minimumvalue;
            public float maximumvalue;

            public int correctedchannel { get { return DmxChannel + 17; } }
        }

    }
}