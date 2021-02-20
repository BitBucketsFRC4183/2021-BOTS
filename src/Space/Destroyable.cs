using Godot;
using System;

public interface Destroyable
{
    Vector2 GlobalPosition { get; set; }
    event Action Destroyed;
}