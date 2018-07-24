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

    public Instantiator CellInstanceManager;

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
        this.CellInstanceManager.CreateNewCell();
    }

    void RenderMessageExchange(string name, string srcId, string dstId)
    {
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
