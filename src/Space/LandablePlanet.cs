using Godot;
using System;

public class LandablePlanet : CelestialBody, LandableThing
{
    [Export]
    public PackedScene LandingScene { get; set; }

    public void Land()
    {
        SceneChanger.Instance.GoToScene(LandingScene);
    }

    public override void _Ready()
    {

    }
}
