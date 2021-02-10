using Godot;
using System.Collections.Generic;
using static Enums;

public class PlayerData : Node
{
    public static PlayerData Instance;

    public Dictionary<Enums.GameResources, int> resources = new Dictionary<Enums.GameResources, int>() {
        {Enums.GameResources.Graprofium, 0},
        {Enums.GameResources.Kamenium, 0},
        {Enums.GameResources.Wooflowium, 0},
        {Enums.GameResources.Efarcium, 0},
        {Enums.GameResources.Xerocrium, 0},
        {Enums.GameResources.Coopertonium, 0},
    };

    public override void _Ready()
    {
        Instance = this;
    }

}
