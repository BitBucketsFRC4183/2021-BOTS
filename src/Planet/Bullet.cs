using Godot;
using System;

public class Bullet : Area2D
{
    [Export]
    int MinSpeed = 500;

    [Export]
    int MaxSpeed = 1000;
    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        Position += new Vector2(1, 0).Rotated(Rotation) * delta * MaxSpeed;
    }
}
