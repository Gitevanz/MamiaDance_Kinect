using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArtDotNet;
using System.Security.Policy;

public class DMX_Controll : MonoBehaviour
{


    public List<ControllerData> controls;
    public List<ToggleObjects> objects;

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
        foreach (var toggletarget in objects)
        {
            if (dmxClient.DMXdata[toggletarget.correctedchannel] == 255) toggletarget.target.SetActive(true); else toggletarget.target.SetActive(false);
        }
        UpdateValues();
    }

    // When the inspector is changed, the target object is checked for changes and all target controls are updated and named with the 'NameMaker' function
    private void OnValidate()
    {
        int targetinteger;
        int fieldamount = 0;
        foreach (var control in controls)
        {
            control.init();
            // Check for the manager type in the Target Object field
            if (control.type == controllerdatatype.particle)
            {
                // Every particle system has 6 fields to control in realtime
                fieldamount = 6;
                // Check and correct if there is a disparity in the lengths of target object list length and the target list in DMX Controll inspector
                targetinteger = Mathf.Abs(control.targetObjectParticle.particleSystems.Count * fieldamount - control.targets.Count);
                for (int i = 0; i < targetinteger; i++)
                {
                    if (control.targetObjectParticle.particleSystems.Count * fieldamount > control.targets.Count)

                    {
                        control.targets.Add(new ControllerData.TargetFields { });
                    }
                    else
                    {
                        control.targets.Remove(control.targets[control.targets.Count - 1]);
                    }
                }
            }
            if (control.type == controllerdatatype.shader)
            {
                // Every shader has 5 fields to control in realtime
                fieldamount = 10;
                // Check and correct if there is a disparity in the lengths of target object list length and the target list in DMX Controll inspector
                targetinteger = Mathf.Abs(control.targetObjectShader.materialList.Count * fieldamount - control.targets.Count);
                for (int i = 0; i < targetinteger; i++)
                {
                    if (control.targetObjectShader.materialList.Count * fieldamount > control.targets.Count)
                    {
                        control.targets.Add(new ControllerData.TargetFields { });
                    }
                    else
                    {
                        control.targets.Remove(control.targets[control.targets.Count - 1]);
                    }
                }
            }
            for (int i = 0; i < control.targets.Count; i++)
            {
                control.targets[i].name = NameMaker(control.type, fieldamount, i);
            }

        }
    }

    public float Mapper(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    public Dictionary<int, string> NameDictionary = new Dictionary<int, string>
    {
        {0, "color 1, RED"},
        {1, "color 1, GREEN"},
        {2, "color 1, BLUE"},
        {3, "color 2, RED"},
        {4, "color 2, GREEN"},
        {5, "color 2, BLUE"},
        {6, "smoothness"},
        {7, "speed"},
        {8, "imageIndex"},
        {9, "control"},
        {20, "size"},
        {21, "speed"},
        {22, "amount"},
        {23, "poofmultiplier"},
        {24, "lifetime"},
        {25, "toggle"}

    };

    // Uses the NameDictionary dictionary to populate the 'name' field for each target to make it easier to find the right target property in the inspector
    public string NameMaker(controllerdatatype type, int fieldamount, int index)
    {
        int nameindex = GetListIndex(index, fieldamount)[1];
        int controlindex = GetListIndex(index, fieldamount)[0];
        if (type == controllerdatatype.particle)
        {
            return controlindex + 1 + ". " + NameDictionary[nameindex + 20];
        }
        else if (type == controllerdatatype.shader)
        {
            return controlindex + 1 + ". " + NameDictionary[nameindex];
        }
        else
        {
            return "Unkown Type";
        }

    }

    // Because targets for all manager list elements are in the same list, a major and minor index are being kept
    // Major index for which element the index points to in the manager and minor index for pointing to the correct property inside the element
    public List<int> GetListIndex (int targetindex, int fieldamount)
    {
        int majorindex = Mathf.FloorToInt(targetindex / fieldamount);
        int subindex = targetindex - majorindex * fieldamount;
        return new List<int> { majorindex , subindex };
    }

    // Update all values to corresponding fields in the manager
    public void UpdateValues()
    {
        int fieldamount = 0;
        foreach (var control in controls)
        {
            // Checking the type of the manager
            if (control.type == controllerdatatype.particle)
            {
                fieldamount = 6;
                for (int i = 0; i < control.targets.Count; i++)
                {
                    int major = GetListIndex(i, fieldamount)[0];
                    int minor = GetListIndex(i, fieldamount)[1];
                    // Channel -1 means not to update the value from dmx
                    if (control.targets[i].DmxChannel != -1)
                    {
                        // Injecting values to the correct properties is done in the manager script, which contains a switch to interpret the 'minor' index
                        control.targetObjectParticle.particleSystems[major].SetInput(control.targets[i].value, minor + 20);
                    }
                }
            }
            // Checking the type of the manager
            if (control.type == controllerdatatype.shader)
            {
                fieldamount = 10;
                for (int i = 0; i < control.targets.Count; i++)
                {
                    int major = GetListIndex(i, fieldamount)[0];
                    int minor = GetListIndex(i, fieldamount)[1];
                    // Channel -1 means not to update the value from dmx
                    if (control.targets[i].DmxChannel != -1)
                    {
                        // Injecting values to the correct properties is done in the manager script, which contains a switch to interpret the 'minor' index
                            control.targetObjectShader.materialList[major].SetInput(control.targets[i].value, minor);
                            
                    }
                }
            }
        }
    }

    // Initializing data types
    // Made public in 'controls' list. ControllerData takes 'Particle manager' or 'Shader manager' as input in 'targetObjects'

    [System.Serializable]
    public class ControllerData
    {
        [HideInInspector]
        public string name;
        public controllerdatatype type;
        public GameObject targetObject;
        public Shader_Controller targetObjectShader;
        public ParticleControl_script targetObjectParticle;
        public List<TargetFields> targets;

        public void init()
        {
            targetObjectShader = targetObject.GetComponent<Shader_Controller>();
            targetObjectParticle = targetObject.GetComponent<ParticleControl_script>();
            if (targetObjectShader != null) type = controllerdatatype.shader;
            else if (targetObjectParticle != null) type = controllerdatatype.particle;
        }

        // TargetFields initializes the values of the target manager and is made public in 'targets' list
        // DMX channel for every controllable value is also defined in TargetFields as 'DmxChannel'
        [System.Serializable]
        public class TargetFields
        {
            [HideInInspector]
            public string name;
            public int DmxChannel;
            public float value;


            [Header("Mapping")]
            public float minimumvalue = 0;
            public float maximumvalue = 1;

            public int correctedchannel { get { return DmxChannel + 17; } }
        }

    }
    [System.Serializable]
    public class ToggleObjects
    {
        public GameObject target;
        public int DmxChannel;
        public int correctedchannel { get { return DmxChannel + 17; } }
    }
}
public enum controllerdatatype
{
    shader,
    particle
}