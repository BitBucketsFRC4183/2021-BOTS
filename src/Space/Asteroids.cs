using Godot;
using System;

public class Asteroids : RigidBody2D
{
    

 
    public override void _Ready()
    {
        
    }
public class Main : Node
{

    [Export]
    public PackedScene Asteroid;

    private int _score;

    private Random _random = new Random();

    public override void _Ready()
    {
    }


    private float RandRange(float min, float max)
    {
        return (float)_random.NextDouble() * (max - min) + min;
    }
}


}
