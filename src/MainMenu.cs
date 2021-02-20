using Godot;
using System;

public class MainMenu : MarginContainer
{
    Loader loaderProgress;

    public override void _Ready()
    {
        Godot.OS.WindowMaximized = true;

        loaderProgress = GetNode<Loader>("TitleContainer/CenterContainer/MenuButtons/HBoxContainer/Loader");
        
        FindNode("NewGame").Connect("pressed", this, nameof(OnNewGamePressed));
        FindNode("Fullscreen").Connect("pressed", this, nameof(OnFullscreenPressed));
        FindNode("Exit").Connect("pressed", this, nameof(OnExitPressed));
    }

    void OnNewGamePressed()
    {
        loaderProgress.loadPlanet();
        ((Button) FindNode("NewGame")).Disabled = true;
    }

    void OnFullscreenPressed()
    {
        OS.WindowFullscreen = !OS.WindowFullscreen;
    }

	void OnExitPressed()
	{
		GetTree().Quit();
	}
}
