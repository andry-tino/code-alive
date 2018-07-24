using System;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Provides functionalities for positioning a mesh.
/// </summary>
public class MeshPositioner
{
    private List<MeshBoundingBox> boxes;

    /// <summary>
    /// Creates a new instance of the <see cref="MeshPositioner"/> class.
    /// </summary>
    public MeshPositioner()
    {
        this.boxes = new List<MeshBoundingBox>();
    }

    /// <summary>
    /// Positions the box in order to create an uniform structure.
    /// </summary>
    /// <param name="box"></param>
    public void Position(MeshBoundingBox box)
    {
        this.boxes.Add(box);
    }
}

public class MeshBoundingBox
{
    double MinX { get; set; }
    double MaxX { get; set; }
    double MinY { get; set; }
    double MaxY { get; set; }
    double MinZ { get; set; }
    double MaxZ { get; set; }
}
