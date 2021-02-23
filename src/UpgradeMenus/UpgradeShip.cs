using Godot;
using System;

public class UpgradeShip : MarginContainer
{
    private InfoPanel Shields, Tank, Speed;
    public override void _Ready()
    {
        Signals.UpgradesChanged += CheckVisibility;
        
        var bg = GetNode<ColorRect>("Background");
        Shields = bg.GetNode<InfoPanel>("Shields");
        Tank = bg.GetNode<InfoPanel>("Tank");
        Speed = bg.GetNode<InfoPanel>("Speed");
        
        Shields.SetPanelType(InfoPanel.UpgradeType.SHIP, Enums.RoverUpgradeType.NULL, Enums.ShipUpgradeType.SHIELDS);
        Tank.SetPanelType(InfoPanel.UpgradeType.SHIP, Enums.RoverUpgradeType.NULL, Enums.ShipUpgradeType.TANK);
        Speed.SetPanelType(InfoPanel.UpgradeType.SHIP, Enums.RoverUpgradeType.NULL, Enums.ShipUpgradeType.SPEED);
        
        Signals.PublishUpgradesChangedEvent();
    }

    public override void _ExitTree()
    {
        Signals.UpgradesChanged -= CheckVisibility;
    }

    private void CheckVisibility()
    {
        Shields.Visible = !PlayerData.Instance.IsUpgradeMaxLevel(Enums.RoverUpgradeType.NULL, Shields.shipUpgrade);
        Tank.Visible = !PlayerData.Instance.IsUpgradeMaxLevel(Enums.RoverUpgradeType.NULL, Tank.shipUpgrade);
        Speed.Visible = !PlayerData.Instance.IsUpgradeMaxLevel(Enums.RoverUpgradeType.NULL, Speed.shipUpgrade);
    }
}
