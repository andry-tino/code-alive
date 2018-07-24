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
    public float PlacementVectorBaseModule = 1.5f;

    #endregion

    private bool requested = false;
    private List<Transform> instances = new List<Transform>(); // Instantiated objects
    private int direction = 0; // 0 1 2 3 4 5 => X+ X- Y+ Y- Z+ Z- (mod6)
    private int modulo = 1;

    void Start()
    {
    }
    
	void Update() {
        if (this.requested)
        {
            Transform copy = Instantiate(this.Object, this.NewDisplacementVector, Quaternion.identity);
            copy.name = this.NewName(this.Object.name ?? "obj");

            // Add to cache
            this.instances.Add(copy);

            // Reset
            this.requested = false;
        }
	}

    public void CreateNewCell()
    {
        this.requested = true;
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
}
