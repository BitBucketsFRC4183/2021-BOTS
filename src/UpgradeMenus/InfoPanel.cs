using Godot;
using System;

public class InfoPanel : Control
{
    private PlayerUpgrades.RoverUpgrades roverUpgrade;
    private PlayerUpgrades.ShipUpgrades shipUpgrade;
    
    public override void _Ready()
    {
        Signals.UpgradesChanged += UpdatePanel;
        
        UpdatePanel();
    }

    public override void _ExitTree()
    {
        Signals.UpgradesChanged -= UpdatePanel;
    }

    public void SetAsRover(PlayerUpgrades.RoverUpgrades r)
    {
        roverUpgrade = r;
        shipUpgrade = PlayerUpgrades.ShipUpgrades.NULL;

        GetNode<Panel>("Background").GetNode<VBoxContainer>("VBox").GetNode<Button>("UpgradeButton").Text =
            "Upgrade Rover!";
    }

    public void SetAsShip(PlayerUpgrades.ShipUpgrades s)
    {
        roverUpgrade = PlayerUpgrades.RoverUpgrades.NULL;
        shipUpgrade = s;
        
        GetNode<Panel>("Background").GetNode<VBoxContainer>("VBox").GetNode<Button>("UpgradeButton").Text =
            "Upgrade Ship!";
    }

    public void UpdatePanel()
    {
        var VBox = GetNode<Panel>("Background").GetNode<VBoxContainer>("VBox");
        var name = VBox.GetNode<Label>("UpgradeName");

        var resources = new [] {"Graprofium", "Kamenium", "Wooflowium", "Efarcium", "Xerocrium", "Coopertonium"};
        
        var u = PlayerData.Instance.upgrades;
        int level;
        UpgradeCost cost;

        if (!roverUpgrade.Equals(PlayerUpgrades.RoverUpgrades.NULL))
        {
            level = u.RoverTech[roverUpgrade];
            cost = u.RoverTechCosts[roverUpgrade][level];
            
            name.Text = u.RoverTechInfo[roverUpgrade][level + 1];

            ProgressBar p;
            for (int i = 0; i < UpgradeCost.NumResourceTypes; i++)
            {
                p = VBox.GetNode<ProgressBar>("Resource" + (i + 1));

                if (cost.Costs[i] == 0)
                {
                    p.Visible = false;
                }
                else
                {
                    p.Visible = true;
                    p.MaxValue = cost.Costs[i];
                    p.MinValue = 0;
                    p.Step = 1;

                    p.GetNode<Label>("Label").Text = resources[i] + ": " + p.MinValue + "/" + p.MaxValue;
                }
            }
        }
        else if(!shipUpgrade.Equals(PlayerUpgrades.ShipUpgrades.NULL))
        {
            
        }
        else GD.Print("Something broke with upgrade panel");
    }

    public void OnUpgradeButtonPressed()
    {
        //Check if the upgrade can be done
        
    }
}
