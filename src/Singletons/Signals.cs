using Godot;
using System;

public class Signals : Node
{
    public static Signals Instance { get; private set; }
    
    [Signal] public delegate void OverlappingResource(GameResource res);
    [Signal] public delegate void NotOverlappingResource();

    [Signal] public delegate void CollectingResource();
    [Signal] public delegate void NotCollectingResource();

    public static event Action UpgradesChanged;
    public static event Action RoverFuelChanged;

    public override void _Ready()
    {
        Instance = this;
    }

    public static void PublishUpgradesChangedEvent()
    {
        UpgradesChanged?.Invoke();
    }

    public static void PublishRoverFuelChangedEvent()
    {
        RoverFuelChanged?.Invoke();
    }
}
