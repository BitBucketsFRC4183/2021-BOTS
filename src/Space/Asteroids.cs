using Godot;
using System;

public class Asteroids : RigidBody2D
{



    public override void _PhysicsProcess(float delta)
    {
        ApplyTorqueImpulse(100);
    }
}
