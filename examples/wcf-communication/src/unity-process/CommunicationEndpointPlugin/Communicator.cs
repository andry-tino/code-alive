using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for exposing the endpoint.
/// </summary>
public class Communicator : MonoBehaviour
{
    private static CommunicationEndpoint communicationEndpoint;

    private static CommunicationEndpoint CommunicationEndpoint
    {
        get
        {
            if (communicationEndpoint == null)
            {
                communicationEndpoint = new CommunicationEndpoint();
            }

            return communicationEndpoint;
        }
    }

    // Use this for initialization
    void Start()
    {
        CommunicationEndpoint.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // Nothing here
    }
}
