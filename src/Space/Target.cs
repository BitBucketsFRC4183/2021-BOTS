using Godot;
using System;

public class Target : SpacePhysicsObject, SpaceDamagable, Destroyable
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
}
