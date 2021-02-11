using Godot;
using System;

public class Missile : SpaceProjectile, SpaceDamagable
{
    public void Hit()
    {
        Destroyed = true;
        QueueFree();
    }
}
