using Godot;
using System;

public class MainMenu : MarginContainer
{
    [Export]
    NodePath loaderProgressPath;

    public override void _Ready()
    {
        Godot.OS.WindowMaximized = true;
        
        FindNode("NewGame").Connect("pressed", this, nameof(OnNewGamePressed));
        FindNode("Fullscreen").Connect("pressed", this, nameof(OnFullscreenPressed));
        FindNode("Exit").Connect("pressed", this, nameof(OnExitPressed));
    }

    void OnNewGamePressed()
    {
        Loader loaderProgress = GetNode<Loader>(loaderProgressPath);
        loaderProgress.loadPlanet();
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
