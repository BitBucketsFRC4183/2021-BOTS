using Godot;
using System;

public class Space : Node2D
{
    public override void _Ready()
    {
        GetNode<UpgradeMain>("MainUpgradeMenu").Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            GetNode<UpgradeMain>("MainUpgradeMenu").Visible = !GetNode<UpgradeMain>("MainUpgradeMenu").Visible;
        }
    }
}
