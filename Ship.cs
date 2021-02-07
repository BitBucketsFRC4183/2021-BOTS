using Godot;
using System;

public class Ship : SpacePhysicsObject
{
    [Export]
    public float Fuel;
    [Export]
    public float MaxFuel;
    [Export]
    public float Speed;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // AddForce(Direction, Speed);
    }
}
