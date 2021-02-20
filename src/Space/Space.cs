using Godot;
using System;

public class Space : Node2D
{
    AcceptDialog tutorial;
    Loader loader;
    public override void _Ready()
    {
        loader = GetNode<Loader>("SpaceHUD/MarginContainer/HBoxContainer/Loader");
        tutorial = GetNode<AcceptDialog>("SpaceHUD/Tutorial");
        if (!PlayerData.Instance.ViewedTutorial)
        {
            tutorial.PopupCentered();
            PlayerData.Instance.ViewedTutorial = false;
            GD.Print("Tutorial is popped");
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
