using Godot;
using System;

public class RoverFuelBar : Control
{
    private ProgressBar bar;
    
    public override void _Ready()
    {
        Signals.RoverFuelChanged += UpdateFuelBar;
        
        bar = GetNode<ProgressBar>("ProgressBar");

        bar.MinValue = 0;
        UpdateFuelBar();
    }

    public override void _ExitTree()
    {
        Signals.RoverFuelChanged -= UpdateFuelBar;
    }

    public void UpdateFuelBar()
    {
        bar.Value = PlayerData.Instance.roverFuel.CurrentFuel;
        bar.MaxValue = PlayerData.Instance.roverFuel.MaxFuel;
    }
}
