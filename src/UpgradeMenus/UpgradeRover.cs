using Godot;
using System;

public class UpgradeRover : MarginContainer
{
    public override void _Ready()
    {
        var bg = GetNode<ColorRect>("Background");
        bg.GetNode<InfoPanel>("Speed").SetPanelType(InfoPanel.UpgradeType.ROVER, Enums.RoverUpgradeType.SPEED);
        bg.GetNode<InfoPanel>("Tank").SetPanelType(InfoPanel.UpgradeType.ROVER, Enums.RoverUpgradeType.TANK);
        bg.GetNode<InfoPanel>("Regen").SetPanelType(InfoPanel.UpgradeType.ROVER, Enums.RoverUpgradeType.REGEN);
        bg.GetNode<InfoPanel>("Inventory").SetPanelType(InfoPanel.UpgradeType.ROVER, Enums.RoverUpgradeType.INVENTORY);
    }
}
