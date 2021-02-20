using Godot;
using System;

public interface LandableThing
{
    [Export]
    string LandingPath { get; set; }
    [Export]
    float LandableRadius { get; set; }
    Vector2 GlobalPosition { get; set; }
    void Land();
}
