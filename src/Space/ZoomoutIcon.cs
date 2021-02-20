using Godot;
using System;

public class ZoomoutIcon : Sprite
{
    [Export]
    public float MinZoomVisible = 10;
    [Export]
    public float MaxZoomVisible = 10;
    public float Zoom = 1;

    [Export]
    // If this is true the icon will set it's visibility based on the zoom of the camera.
    public bool OverrideVisibility = true;

    [Export]
    // If this is true the global rotation of the icon will always be 0.
    public bool OverrideRotation = true;

    [Export]
    public Vector2 BaseScale = Vector2.One;

    public Camera2D ActiveCamera;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Godot.Collections.Array Cameras = GetTree().GetNodesInGroup("SpaceCameras");
        ActiveCamera = (Camera2D)Cameras[0];
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Zoom = ActiveCamera.Zoom.x;
        if (Zoom >= MinZoomVisible && Zoom <= MaxZoomVisible)
        {
            if (OverrideVisibility)
            {
                Visible = true;
            }
            if (OverrideRotation)
            {
                GlobalRotation = 0;
            }
            Scale = BaseScale * ActiveCamera.Zoom;
        }
        else
        {
            if (OverrideVisibility)
            {
                Visible = false;
            }
        }
    }
}
