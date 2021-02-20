using Godot;
using System;

public class UpgradeShip : MarginContainer
{
    private InfoPanel Shields;
    public override void _Ready()
    {
        Signals.UpgradesChanged += CheckVisibility;
        
        var bg = GetNode<ColorRect>("Background");
        Shields = bg.GetNode<InfoPanel>("Shields");
        
        Shields.SetPanelType(InfoPanel.UpgradeType.SHIP, Enums.RoverUpgradeType.NULL, Enums.ShipUpgradeType.SHIELDS);
        
        Signals.PublishUpgradesChangedEvent();
    }

    public override void _ExitTree()
    {
        Signals.UpgradesChanged -= CheckVisibility;
    }

    private void CheckVisibility()
    {
        Shields.Visible = !PlayerData.Instance.IsUpgradeMaxLevel(Enums.RoverUpgradeType.NULL, Shields.shipUpgrade);
    }
}
