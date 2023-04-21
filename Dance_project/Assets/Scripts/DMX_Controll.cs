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
                Debug.Log("i is " + i);
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
        {6, "Smoothness"},
        {7, "Speed"},
        {8, "Image index"},
        {9, "Control"},
        {20, "Size"},
        {21, "Speed"},
        {22, "Amount"},
        {23, "Poof"},
        {24, "Lifetime"},
        {25, "Toggle"}

    };

    public string NameMaker(string type, int fieldamount, int index)
    {
        int fieldindex = 1;
        int fieldsubtract;
        int nameindex;
        for (int i = 0; index + 1 > fieldamount * fieldindex; i++)
        {
            fieldindex = i;
        }
        fieldsubtract = (fieldindex - 1) * fieldamount;
        nameindex = index - fieldsubtract;
        Debug.Log(nameindex);
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