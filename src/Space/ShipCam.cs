using Godot;
using System;

public class ShipCam : Camera2D
{
    public float ZoomRate = 1.2f;
    public float MinZoom = 0.5f;
    public float MaxZoom = 5000f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("space_camera_zoomout"))
        {
            Zoom = Zoom * ZoomRate;
        }
        if (Input.IsActionPressed("space_camera_zoomin"))
        {
            Zoom = Zoom / ZoomRate;
        }
        if (Zoom.x < MinZoom)
        {
            Zoom = new Vector2(MinZoom, MinZoom);
        }
        else if (Zoom.x > MaxZoom)
        {
            Zoom = new Vector2(MaxZoom, MaxZoom);
        }
    }
}