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
        UpdateValues();
    }

    // When the inspector is changed, the target object is checked for changes and all target controls are updated
    private void OnValidate()
    {
        int targetinteger;
        int fieldamount = 0;
        string targetobjecttype = "none";
        foreach (var control in controls)
        {
            // Check for the manager type in the Target Object field
            if (control.targetObject.name == "Particle Manager")
            {
                targetobjecttype = "particle";
                // Every particle system has 6 fields to control in realtime
                fieldamount = 6;
                // Check and correct if there is a disparity in the lengths of target object list length and the target list in DMX Controll inspector
                targetinteger = Mathf.Abs(control.targetObject.GetComponent<ParticleControl_script>().particleSystems.Count * fieldamount - control.targets.Count);
                for (int i = 0; i < targetinteger; i++)
                {
                    if (control.targetObject.GetComponent<ParticleControl_script>().particleSystems.Count * fieldamount > control.targets.Count)
                    {
                        control.targets.Add(new ControllerData.TargetFields { });
                    }
                    else
                    {
                        control.targets.Remove(control.targets[control.targets.Count - 1]);
                    }
                }
            }
            if (control.targetObject.name == "Shader Manager")
            {
                targetobjecttype = "shader";
                // Every shader has 5 fields to control in realtime
                fieldamount = 10;
                // Check and correct if there is a disparity in the lengths of target object list length and the target list in DMX Controll inspector
                targetinteger = Mathf.Abs(control.targetObject.GetComponent<Shader_Controller>().materialList.Count * fieldamount - control.targets.Count);
                for (int i = 0; i < targetinteger; i++)
                {
                    if (control.targetObject.GetComponent<Shader_Controller>().materialList.Count * fieldamount > control.targets.Count)
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
                control.targets[i].name = NameMaker(targetobjecttype, fieldamount, i);
            }

        }
    }

    public float Mapper(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    public Dictionary<int, string> NameDictionary = new Dictionary<int, string>
    {
        {0, "Color 1, RED"},
        {1, "Color 1, GREEN"},
        {2, "Color 1, BLUE"},
        {3, "Color 2, RED"},
        {4, "Color 2, GREEN"},
        {5, "Color 2, BLUE"},
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
   
    public List<int> GetListIndex (int targetindex, int fieldamount)
    {
        int majorindex = Mathf.FloorToInt(targetindex / fieldamount);
        int subindex = targetindex - majorindex * fieldamount;
        return new List<int> { majorindex , subindex };
    }

    public string NameMaker(string type, int fieldamount, int index)
    {
        int nameindex = GetListIndex(index, fieldamount)[1];
        if (type == "particle")
        {
            return NameDictionary[nameindex + 20];
        }
        else if (type == "shader")
        {
            return NameDictionary[nameindex];
        }
        else
        {
            return "Unkown Type";
        }

    }

    // Update all values to corresponding fields in the manager
    public void UpdateValues()
    {
        int fieldamount = 0;
        foreach (var control in controls)
        {
            if (control.targetObject.name == "Particle Manager")
            {
                fieldamount = 6;
                for (int j = 0; j < control.targets.Count; j++)
                {
                    int major = GetListIndex(j, fieldamount)[0];
                    int minor = GetListIndex(j, fieldamount)[1];
                    Debug.Log("List index: " + major + ", " + minor);
                }
            }
            if (control.targetObject.name == "Shader Manager")
            {
                fieldamount = 10;
                for (int j = 0; j < control.targets.Count; j++)
                {
                    int major = GetListIndex(j, fieldamount)[0];
                    int minor = GetListIndex(j, fieldamount)[1];
                    if (control.targets[j].name.Contains("color"))
                    {
                        /*if (controls[i].targets[j].name.Contains("1"))
                        {
                            controls[i].targetObject.GetComponent<Shader_Controller>().materialList[i].GetType().GetField("color1").SetValue(controls[i].targets[j].name, new Color(controls[i].targets[j].value/255f, controls[i].targets[j + 1].value / 255f, controls[i].targets[j + 2].value / 255f));
                        } else
                        {
                            controls[i].targetObject.GetComponent<Shader_Controller>().materialList[i].GetType().GetField("color2").SetValue(controls[i].targets[j].name, new Color(controls[i].targets[j].value / 255f, controls[i].targets[j + 1].value / 255f, controls[i].targets[j + 2].value / 255f));
                        }*/ 
                        Debug.Log("List index: " + major + ", " + minor);
                        j++;
                        j++;
                        Debug.Log("color skipped");
                    } else
                    {
                        control.targetObject.GetComponent<Shader_Controller>().materialList[major].GetType().GetProperty(NameDictionary[minor]).SetValue(NameDictionary[minor], control.targets[j].value);
                        Debug.Log("List index: " + major + ", " + minor);
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
        public GameObject targetObject;
        public List<TargetFields> targets;

        // TargetFields initializes the values of the target manager and is made public in 'targets' list
        // DMX channel for every controllable value is also defined in TargetFields as 'DmxChannel'
        [System.Serializable]
        public class TargetFields
        {
            [HideInInspector]
            public string name;
            public int DmxChannel;
            public float value;

            //public List<float> valuelist;

            [Header("Mapping")]
            public float minimumvalue = 0;
            public float maximumvalue = 1;

            public int correctedchannel { get { return DmxChannel + 17; } }
        }

    }
}