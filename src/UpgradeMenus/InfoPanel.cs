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

        if (isRover())
        {
            level = u.RoverTech[roverUpgrade];
            cost = u.RoverTechCosts[roverUpgrade][level + 1];
            
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
        else if(!isRover())
        {
            
        }
        else GD.Print("Something broke with upgrade panel");
    }

    public void OnUpgradeButtonPressed()
    {
        var r = PlayerData.Instance.resources;
        var u = PlayerData.Instance.upgrades;
        var c = isRover() ? u.RoverTechCosts[roverUpgrade][u.RoverTech[roverUpgrade] + 1] : u.ShipTechCosts[shipUpgrade][u.ShipTech[shipUpgrade] + 1];

        var res = new[]
        {
            Enums.GameResources.Graprofium, Enums.GameResources.Kamenium, Enums.GameResources.Wooflowium,
            Enums.GameResources.Efarcium, Enums.GameResources.Xerocrium, Enums.GameResources.Coopertonium
        };
        
        //Check if the upgrade can be done (if not, return)
        for (int i = 0; i < UpgradeCost.NumResourceTypes; i++)
        {
            if (c.Costs[i] != 0 && r[res[i]] < c.Costs[i]) return;
        }
        GD.Print("Player has enough resources!");
        
        //Change the player's tech level
        if (isRover()) u.UpgradeRover(roverUpgrade);
        else u.UpgradeShip(shipUpgrade);
        
        //Deplete player's resources
        for (int i = 0; i < UpgradeCost.NumResourceTypes; i++) r[res[i]] -= c.Costs[i];
        
        //Update the upgrade scenes
        Signals.PublishUpgradesChangedEvent();
    }

    private bool isRover()
    {
        return roverUpgrade != PlayerUpgrades.RoverUpgrades.NULL;
    }
}
