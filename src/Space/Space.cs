using Godot;
using System;

public class Space : Node2D
{
    AcceptDialog tutorial;
    Loader loader;
    UpgradeMain upgradeMenu;

    public override void _Ready()
    {
        loader = GetNode<Loader>("SpaceHUD/MarginContainer/HBoxContainer/Loader");
        tutorial = GetNode<AcceptDialog>("SpaceHUD/Tutorial");
	upgradeMenu = GetNode<CanvasLayer>("SpaceHUD").GetNode<UpgradeMain>("MainUpgradeMenu");

	upgradeMenu.Visible = false;

        if (!PlayerData.Instance.ViewedTutorial)
        {
            tutorial.PopupCentered();
            PlayerData.Instance.ViewedTutorial = false;
            GD.Print("Tutorial is popped");
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            upgradeMenu.Visible = !upgradeMenu.Visible;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }

    public void LoadPlanet(string filePath)
    {
        loader.loadPlanet(filePath);
    }
}
