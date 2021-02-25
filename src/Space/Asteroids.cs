using Godot;
using System;

public class Asteroids : SpacePhysicsObject, SpaceDamagable, Destroyable
{
    public event Action Destroyed;

    public void Hit()
    {
        Destroyed?.Invoke();
        QueueFree();
    }

    public override void OnCollision(Node2D body)
    {
        Hit();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Rotate(0.5f * delta);
    }
}
