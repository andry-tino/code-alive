using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CodeAlive.Communication;

/// <summary>
/// Root object of the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    private Communicator communicator;

    public int An;

	// Use this for initialization
	void Start () {
        this.InitializeCommunicator();
	}

    private void InitializeCommunicator()
    {
        this.communicator = new Communicator();
    }
}
