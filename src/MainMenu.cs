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
        FindNode("NewGame").Connect("pressed", this, nameof(OnNewGamePressed));
        FindNode("Continue").Connect("pressed", this, nameof(OnContinuePressed));
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

    void OnContinuePressed()
    {
        //
    }

    void OnExitPressed()
    {
        GetTree().Quit();
    }
}
