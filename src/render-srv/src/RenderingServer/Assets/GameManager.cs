/// <summary>
/// Andrea Tino - 2018
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CodeAlive.Communication;
using CodeAlive.Communication.Eventing;

/// <summary>
/// Root object of the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Parameters

    /// <summary>
    /// The port to use when creating the server
    /// </summary>
    public short ServerPort = 8000;

    /// <summary>
    /// The hostname to use when running the server.
    /// </summary>
    public string ServerHostName = "localhost";

    /// <summary>
    /// The instantiator to use.
    /// </summary>
    public Instantiator CellInstanceManager;

    /// <summary>
    /// The connector instantiator to use.
    /// </summary>
    public ConnectorInstantiator TubeInstanceManager;

    /// <summary>
    /// The message instantiator to use.
    /// </summary>
    public MsgInstantiator MsgInstanceManager;

    #endregion

    private Communicator communicator;

	// Use this for initialization
	void Start() {
        this.InitializeCommunicator();
        this.AttachEvents();

        Debug.Log($"Renderer server is ready listening on: {this.Address}!");
	}

    void OnDestroy()
    {
        this.DetachEvents();
        this.communicator.Dispose();

        Debug.Log("Renderer server disposed!");
    }

    void InitializeCommunicator()
    {
        this.communicator = new Communicator(this.ServerHostName, this.ServerPort);

        try
        {
            this.communicator.Initialize();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Could not initialize the communicator at {this.ServerHostName}:{this.ServerPort}.", e);
        }
    }

    void AttachEvents()
    {
        this.communicator.DiagnosticOccurred += OnDiagnosticOccurred;
        this.communicator.MessageExchangeOccurred += OnMessageExchangeOccurred;
        this.communicator.NewCellOccurred += OnNewCellOccurred;
    }

    void DetachEvents()
    {
        this.communicator.DiagnosticOccurred -= OnDiagnosticOccurred;
        this.communicator.MessageExchangeOccurred -= OnMessageExchangeOccurred;
        this.communicator.NewCellOccurred -= OnNewCellOccurred;
    }

    #region Render operations

    void RenderNewCell(string id)
    {
        this.CellInstanceManager.CreateNew(id);

        // Making an assumption for now that all relations are tied to the root object (simplification)
        this.TubeInstanceManager.CreateNew("Cell", id ?? "Invalid");
    }

    void RenderMessageExchange(string name, string srcId, string dstId)
    {
        this.MsgInstanceManager.CreateNew("MsgEx", srcId, dstId);
    }

    #endregion

    #region Event handlers

    void OnNewCellOccurred(NewCellRenderingEvent e)
    {
        Debug.Log($"New-Cell Event received - Id: {e.Id}");

        this.RenderNewCell(e.Id);
    }

    void OnMessageExchangeOccurred(MessageExchangeRenderingEvent e)
    {
        Debug.Log($"Message-Exchange Event received - Name: {e.InvocationName}, Src: {e.SourceId}, Dst: {e.DestinationId}");

        this.RenderMessageExchange(e.InvocationName, e.SourceId, e.DestinationId);
    }

    void OnDiagnosticOccurred(DiagnosticRenderingEvent e)
    {
        Debug.Log($"Diagnostic Event received - Content: {e.Content}");
    }

    #endregion

    string Address => $"{this.ServerHostName}:{this.ServerPort}";
}
