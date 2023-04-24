 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl_script : MonoBehaviour
{
    public List<ParticleData> particleSystems;


    // Start is called before the first frame update
    void Awake()
    {
        // Initialize list elements
        foreach (var systems in particleSystems)
        {
            // Set up modules
            systems.InitializeModules();

            // Pass the ParticleData variables to particle system
            systems.UpdateValues();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update all variables to all systems
        // Optimize? Mark changed variables if you want to optimize and only update those

        foreach (var systems in particleSystems)
        {
            systems.UpdateValues();
        }
    }

    // Update particle system name in list when something in inspector is updated
    private void OnValidate()
    {
        for (int i = 0; i < particleSystems.Count; i++)
        {
            particleSystems[i].name = particleSystems[i].system.name;
        }
    }

    // Create public method to update ParticleData list values by id from QLC


    /// <summary>
    /// Particle system data object to interact with within list
    /// </summary>
    [System.Serializable]
    public class ParticleData
    {
        [HideInInspector] public string name;
        public ParticleSystem system;

        // Particle system modules needed for the variables to change
        public ParticleSystem.MainModule main;
        public ParticleSystem.EmissionModule emission;
        [HideInInspector] public ParticleSystemRenderer renderer;

        // Run this only once in Awake
        public void InitializeModules()
        {
            main = system.main;
            emission = system.emission;
            renderer = system.GetComponent<ParticleSystemRenderer>();
            if (overridematerial != null) renderer.material = overridematerial;
        }


        // Variables to change. Hide in inspector if you want to drive this from elsewhere? [HideInInspector]
        [Range(0, 1f)] public float size;
        [Range(0, 1f)] public float speed;
        [Range(0, 1f)] public float amount;
        public bool poofmultiplier;
        [Range(0, 1f)] public float lifetime;
        public Material overridematerial;

        
        public bool toggle; // on or off like boolean 
        

        // Run this every time values are changed (or Update() if you're lazy)
        public void UpdateValues()
        {
            // Go through all variables and update
            main.startSize = size * 10f;
            main.simulationSpeed = speed * 10f;
            main.startLifetime = lifetime * 10f;
            emission.rateOverTime = amount * 10f;
            if (poofmultiplier) emission.rateOverTime = amount * 100000;
            emission.enabled = toggle;
        }
        public void SetInput(float value, int index)
        {
            switch (index)
            {
                case 20:
                    size = value; break;
                case 21:
                    speed = value; break;
                case 22:
                    amount = value; break;
                case 23:
                    if (value >= 0.5) poofmultiplier = true; else poofmultiplier = false; break;
                case 24:
                    lifetime = value; break;
                case 25:
                    if (value >= 0.5) toggle = true; else toggle = false; break;
            }
        }
    }
}
