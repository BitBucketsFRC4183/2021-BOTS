using Godot;
using System;

public class SpaceShuttleArea : Area2D
{
    [Export]
    PackedScene spaceScene;

    public void _on_SpaceShuttleArea_body_entered(Node body)
    {
        SceneChanger.Instance.GoToScene(spaceScene);
    }
}
