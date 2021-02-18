using Godot;
using System;

public class MainMenu : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Godot.OS.WindowMaximized = true;
        
        FindNode("NewGame").Connect("pressed", this, nameof(OnNewGamePressed));
        FindNode("Fullscreen").Connect("pressed", this, nameof(OnFullscreenPressed));
        FindNode("Exit").Connect("pressed", this, nameof(OnExitPressed));
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    void OnNewGamePressed()
    {
        GetTree().ChangeScene("res://src/Planet/Planet.tscn");
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
