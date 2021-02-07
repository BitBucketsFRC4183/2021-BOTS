using Godot;
using System;

public class Missile : SpaceProjectile, SpaceDamagable
{
    public void hit()
    {
        Destroyed = true;
        QueueFree();
    }
}
