using Godot;
using System;

public class Ship : SpacePhysicsObject, SpaceDamagable
{
    [Export]
    public float Fuel;
    [Export]
    public float MaxFuel;
    [Export]
    public float Speed;
    [Export]
    public int ShieldStrength;

    public bool CanLaserFire = true;


    public enum Weapon
    {
        Missile,
        Laser,
        Railgun
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // AddForce(Direction, Speed);
    }
    public void FireWeapon(Node2D target, Weapon weapon)
    {
        if (weapon == Weapon.Missile)
        {
            // fire missile
        }
        else if (weapon == Weapon.Laser)
        {
            if (target is SpaceDamagable damagable)
            {
                damagable.Hit();
                CanLaserFire = false;
            }
        }
        else if (weapon == Weapon.Railgun)
        {
            // fire rail
        }
    }

    public void Hit()
    {
        if (ShieldStrength > 0)
        {
            ShieldStrength -= 1;
        }
        else
        {
            Destroyed = true;
            QueueFree();
        }
    }
}
