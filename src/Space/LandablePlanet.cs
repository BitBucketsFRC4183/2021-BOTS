using Godot;
using System;

public class LandablePlanet : CelestialBody, LandableThing
{
    [Export]
    public string LandingPath { get; set; }
    [Export]
    public float LandableRadius { get; set; }

    Space space;
    public override void _Ready()
    {
        base._Ready();
        space = (Space)GetTree().Root.GetNode<Space>("Space");
    }

    public void Land()
    {
        space.LoadPlanet(LandingPath);
    }
}