using Godot;
using System;

public class SpacePhysicsObject : KinematicBody2D
{
    public bool Destroyed = false;
    [Export]
    public float Mass = 1;
    [Export]
    public Vector2 Velocity = Vector2.Zero;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        // Placeholder
        MoveAndCollide(Velocity);
    }

    public void OnCollision()
    {

    }

    public void AddForce(float direction, float amount)
    {
        // Placeholder
        Vector2 directionVector = new Vector2(1, 0).Rotated(direction);

        Velocity += (directionVector * amount / Mass);
    }
    public void AddForce(Vector2 direction, float amount)
    {
        // Placeholder
        Velocity += (direction * amount / Mass);
    }
}