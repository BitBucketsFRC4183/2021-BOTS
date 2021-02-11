using Godot;
using System;

public class SpacePhysicsObject : KinematicBody2D
{
    public bool Destroyed = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {

    }

    public void OnCollision()
    {

    }

    public void AddForce(float direction, float amount)
    {

    }
    public void AddForce(Vector2 direction, float amount)
    {

    }
}
