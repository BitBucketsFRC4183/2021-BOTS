using Godot;
using System;

public class Target : SpacePhysicsObject, SpaceDamagable
{
    public void Hit()
    {
        Destroyed = true;
        QueueFree();
    }
    public void OnCollision()
    {
        base.OnCollision();
        Hit();
    }
}
