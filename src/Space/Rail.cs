using Godot;
using System;

public class Rail : SpaceProjectile, SpaceDamagable
{
    public void Hit()
    {
        Destroyed = true;
        QueueFree();
    }
}
