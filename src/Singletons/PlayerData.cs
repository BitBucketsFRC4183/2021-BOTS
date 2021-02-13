using Godot;
using Godot.Collections;

public class PlayerData : Node
{
    public static PlayerData Instance;

    public PlayerResources resources = new PlayerResources();
    public PlayerUpgrades upgrades = new PlayerUpgrades();

    public override void _Ready()
    {
        Instance = this;
    }

}

