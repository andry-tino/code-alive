using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public Transform Object;

    private bool requested = false;

	// Use this for initialization
	void Update() {
        if (this.requested)
        {
            Transform copy = Instantiate(this.Object, new Vector3(0, 0, 3), Quaternion.identity);

            // Reset
            this.requested = false;
        }
	}

    public void CreateNewCell()
    {
        this.requested = true;
    }
}
