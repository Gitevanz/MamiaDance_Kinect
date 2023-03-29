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
        foreach (var particleSystemInList in particleSystems)
        {
            // Set up modules
            particleSystemInList.InitializeModules();

            // Pass the ParticleData variables to particle system
            particleSystemInList.UpdateValues();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update all variables to all systems
        // Optimize? Mark changed variables if you want to optimize and only update those

        foreach (var systemData in particleSystems)
        {
            systemData.UpdateValues();
        }
    }

    // Create public method to update ParticleData list values by id from QLC


    /// <summary>
    /// Particle system data object to interact with within list
    /// </summary>
    [System.Serializable]
    public class ParticleData
    {
        public string id;
        public ParticleSystem system;


        // Particle system modules neede for the variables to change
        public ParticleSystem.MainModule main;
        public ParticleSystem.EmissionModule emission;
        public ParticleSystem.ShapeModule shape;

        // Run this only once in Awake
        public void InitializeModules()
        {
            main = system.main;
            emission = system.emission;
            shape = system.shape;

            // Use particle system's values as base?
            size = main.startSize.constant;
            lifetime = main.startLifetime.constant;
        }


        // Variables to change. Hide in inspector if you want to drive this from elsewhere? [HideInInspector]
        public float size;
        public float lifetime;

        // Implement these:
        public float brightness;
        public float amount;
        public Color color;
        public float toggle; // on or off like boolean
        

        // Run this every time values are changed (or Update() if you're lazy)
        public void UpdateValues()
        {
            // Go through all variables and update
            main.startSize = size;
            main.startLifetime = lifetime;
        }
    }
}
