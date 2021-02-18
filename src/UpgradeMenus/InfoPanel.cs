using Godot;
using System;

public class InfoPanel : Control
{
    public enum UpgradeType {ROVER, SHIP}

    [Export] public UpgradeType PanelUpgradeType = UpgradeType.ROVER;
    
    private Enums.RoverUpgradeType roverUpgrade;
    private Enums.ShipUpgradeType shipUpgrade;
    
    public override void _Ready()
    {
        Signals.UpgradesChanged += UpdatePanel;
        
        UpdatePanel();
    }

    public override void _ExitTree()
    {
        Signals.UpgradesChanged -= UpdatePanel;
    }

    public void SetPanelType(UpgradeType type, Enums.RoverUpgradeType r = Enums.RoverUpgradeType.NULL,
        Enums.ShipUpgradeType s = Enums.ShipUpgradeType.NULL)
    {
        PanelUpgradeType = type;
        roverUpgrade = r;
        shipUpgrade = s;
        
        GetNode<Panel>("Background").GetNode<VBoxContainer>("VBox").GetNode<Button>("UpgradeButton").Text =
            "Upgrade " + (type.Equals(UpgradeType.ROVER) ? "Rover" : "Ship") + "!";
    }

    public void UpdatePanel()
    {
        var VBox = GetNode<Panel>("Background").GetNode<VBoxContainer>("VBox");
        var name = VBox.GetNode<Label>("UpgradeName");

        var resources = new [] {"Graprofium", "Kamenium", "Wooflowium", "Efarcium", "Xerocrium", "Coopertonium"};
        
        if (isRover())
        {
            
        }
        else if(!isRover())
        {
            
        }
        else GD.Print("Something broke with upgrade panel");
    }

    public void OnUpgradeButtonPressed()
    {
        if (PlayerData.Instance.CanUpgrade(roverUpgrade, shipUpgrade))
        {
            PlayerData.Instance.UpgradeTech(roverUpgrade, shipUpgrade);
        }
    }

    private bool isRover()
    {
        return PanelUpgradeType.Equals(UpgradeType.ROVER);
    }
}
