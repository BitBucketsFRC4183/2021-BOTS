using Godot;
using System;

public class InfoPanel : Control
{
    public enum UpgradeType {ROVER, SHIP}

    [Export] public UpgradeType PanelUpgradeType = UpgradeType.ROVER;
    
    public Enums.RoverUpgradeType roverUpgrade;
    public Enums.ShipUpgradeType shipUpgrade;
    
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
        var u = PlayerData.Instance.GetUpgrade(roverUpgrade, shipUpgrade);
        ProgressBar p;
        
        name.Text = u.Name;
        foreach (Enums.GameResources r in Enum.GetValues(typeof(Enums.GameResources)))
        { 
            p = VBox.GetNode<ProgressBar>("Resource" + ((int) r + 1));

            if (u.Cost[r] != 0)
            { 
                p.MaxValue = u.Cost[r];
                p.Step = 1;
                p.MinValue = 0;
                p.Visible = true;
                p.Value = PlayerData.Instance.resources[r] >= u.Cost[r] ? u.Cost[r] : PlayerData.Instance.resources[r];
                p.GetNode<Label>("Name").Text = resources[(int) r] + ": " + p.Value + " / " + p.MaxValue;
            }
            else p.Visible = false;
        }
    }

    public void OnUpgradeButtonPressed()
    {
        if (PlayerData.Instance.CanUpgrade(roverUpgrade, shipUpgrade))
        {
            PlayerData.Instance.UpgradeTech(roverUpgrade, shipUpgrade);
            Signals.PublishUpgradesChangedEvent();
        }
    }

    private bool isRover()
    {
        return PanelUpgradeType.Equals(UpgradeType.ROVER);
    }
}
