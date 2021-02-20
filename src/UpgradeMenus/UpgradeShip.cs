using Godot;
using System;

public class UpgradeShip : MarginContainer
{
    public override void _Ready()
    {
        var bg = GetNode<ColorRect>("Background");
        bg.GetNode<InfoPanel>("Shields").SetPanelType(InfoPanel.UpgradeType.SHIP, Enums.RoverUpgradeType.NULL, Enums.ShipUpgradeType.SHIELDS);
        
        Signals.PublishUpgradesChangedEvent();
    }
}
