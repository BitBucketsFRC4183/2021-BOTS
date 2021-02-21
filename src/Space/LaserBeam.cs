using Godot;
using System;

public class LaserBeam : Line2D
{
    public float Zoom = 1;
    public Camera2D ActiveCamera;

    [Export]
    public float BaseWidth = 2;

    public override void _Ready()
    {
        Godot.Collections.Array Cameras = GetTree().GetNodesInGroup("SpaceCameras");
        ActiveCamera = (Camera2D)Cameras[0];
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (Visible)
        {
            Zoom = ActiveCamera.Zoom.x;
            GlobalRotation = 0;
            Width = Zoom * BaseWidth;
        }
    }
}
