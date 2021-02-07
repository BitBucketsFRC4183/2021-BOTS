using Godot;
using System;

public class Rail : SpaceProjectile, SpaceDamagable
{
    public void hit()
    {
        Destroyed = true;
        QueueFree();
    }
}
