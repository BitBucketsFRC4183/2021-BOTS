using Godot;

public class PlayerData : Node
{
    public static PlayerData Instance;

    public PlayerResources resources = new PlayerResources();

    public override void _Ready()
    {
        Instance = this;
    }

}
