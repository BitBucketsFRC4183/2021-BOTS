using Godot;
using System;

public class FuelBar : ProgressBar
{
    public SceneTree Tree;
    public Ship PlayerShip;
    public override void _Ready()
    {
        Tree = GetTree();
        PlayerShip = (Ship)Tree.Root.GetNode<Ship>("Space/PlayerShip");
    }
    public override void _Process(float delta)
    {
        if (PlayerShip != null)
        {
            Value = PlayerShip.Fuel;
        }
    }
}
