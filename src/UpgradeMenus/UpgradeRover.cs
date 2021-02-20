using Godot;
using System;

public class UpgradeRover : MarginContainer
{
    private InfoPanel Speed, Tank, Regen, Inventory;
    public override void _Ready()
    {
        Signals.UpgradesChanged += CheckVisibility;
        
        var bg = GetNode<ColorRect>("Background");
        Speed = bg.GetNode<InfoPanel>("Speed");
        Tank = bg.GetNode<InfoPanel>("Tank");
        Regen = bg.GetNode<InfoPanel>("Regen");
        Inventory = bg.GetNode<InfoPanel>("Inventory");
        
        Speed.SetPanelType(InfoPanel.UpgradeType.ROVER, Enums.RoverUpgradeType.SPEED);
        Tank.SetPanelType(InfoPanel.UpgradeType.ROVER, Enums.RoverUpgradeType.TANK);
        Regen.SetPanelType(InfoPanel.UpgradeType.ROVER, Enums.RoverUpgradeType.REGEN);
        Inventory.SetPanelType(InfoPanel.UpgradeType.ROVER, Enums.RoverUpgradeType.INVENTORY);
        
        Signals.PublishUpgradesChangedEvent();
    }
    
    public override void _ExitTree()
    {
        Signals.UpgradesChanged -= CheckVisibility;
    }

    private void CheckVisibility()
    {
        Speed.Visible = !PlayerData.Instance.IsUpgradeMaxLevel(Speed.roverUpgrade, Enums.ShipUpgradeType.NULL);
        Tank.Visible = !PlayerData.Instance.IsUpgradeMaxLevel(Tank.roverUpgrade, Enums.ShipUpgradeType.NULL);
        Regen.Visible = !PlayerData.Instance.IsUpgradeMaxLevel(Regen.roverUpgrade, Enums.ShipUpgradeType.NULL);
        Inventory.Visible = !PlayerData.Instance.IsUpgradeMaxLevel(Inventory.roverUpgrade, Enums.ShipUpgradeType.NULL);
    }
}
