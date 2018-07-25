/// <summary>
/// Andrea Tino - 2018
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

/// <summary>
/// Provides API for instantiating pbjects on scene.
/// </summary>
public class Instantiator : MonoBehaviour
{
    #region Parameters

    /// <summary>
    /// The object to clone.
    /// </summary>
    public Transform Object;

    /// <summary>
    /// The modulo of the placement vector.
    /// The placement vector will be a rough multiple of this value.
    /// </summary>
    public float PlacementVectorBaseModule = 2.5f;

    #endregion

    // Request handling
    private InstantiationRequest requested = null;

    private List<Transform> instances = new List<Transform>(); // Instantiated objects
    private int direction = 0; // 0 1 2 3 4 5 => X+ X- Y+ Y- Z+ Z- (mod6)
    private int modulo = 1; // Used for generating spherical layers
    private System.Random rnd = new System.Random();

    void Start()
    {
    }
    
	void Update() {
        if (this.requested != null)
        {
            Transform copy = Instantiate(this.Object, this.NewDisplacementVector, Quaternion.identity);

            // Assign new name
            // 1. Requested name
            // 2. Otherwise, template object prefixed name
            // 3. Otherwise default prefixed name
            copy.name = this.requested.Name ?? this.NewName(this.Object.name ?? "obj");

            // Add to cache
            this.instances.Add(copy);

            // Reset
            this.requested = null;
        }
	}

    public void CreateNew(string name = null)
    {
        this.requested = new InstantiationRequest()
        {
            Name = name
        };
    }

    private Vector3 NewDisplacementVector
    {
        get
        {
            var pv = new Vector3(0, 0, 0);

            switch (this.direction)
            {
                case 0: pv += new Vector3(1, 0, 0); break;
                case 1: pv += new Vector3(-1, 0, 0); break;
                case 2: pv += new Vector3(0, 1, 0); break;
                case 3: pv += new Vector3(0, -1, 0); break;
                case 4: pv += new Vector3(0, 0, 1); break;
                case 5: pv += new Vector3(0, 0, -1); break;
                default:
                    pv += new Vector3(0, 0, 0); break;
            }

            pv = this.modulo * this.PlacementVectorBaseModule * pv;

            // Tweak the vector so we display not a boring tetraheadrical structure
            float displacementFactor = 0.5f * this.PlacementVectorBaseModule;
            pv += displacementFactor * new Vector3((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);

            // Update placement quantities
            this.UpdateDirectionAndModulo();

            return pv;
        }
    }

    private string NewName(string prefix) => $"{prefix}-{this.instances.Count + 1}";

    private void UpdateDirectionAndModulo()
    {
        this.direction = (this.direction + 1) % 6;

        if (this.direction == 0)
        {
            this.modulo++;
        }
    }

    #region Types

    private class InstantiationRequest
    {
        public string Name { get; set; }
    }

    #endregion
}
