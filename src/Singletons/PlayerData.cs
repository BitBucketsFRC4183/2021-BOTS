using Godot;
using Godot.Collections;

public class PlayerData : Node
{
    public static PlayerData Instance;

    public InventoryResources resources = new InventoryResources();
    public PlayerUpgrades upgrades = new PlayerUpgrades();

    public override void _Ready()
    {
        Instance = this;
    }

}

