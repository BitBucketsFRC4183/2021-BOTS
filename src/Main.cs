using Godot;
using System;

public class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Godot.OS.WindowMaximized = true;
	}

}
