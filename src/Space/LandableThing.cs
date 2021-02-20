using Godot;
using System;

public interface LandableThing
{
    [Export]
    PackedScene LandingScene { get; set; }
    void Land();
}
