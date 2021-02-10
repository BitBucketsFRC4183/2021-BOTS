using Godot;
using System;

public class Player : KinematicBody2D
{
    const int SPEED = 400;
    Vector2 motion = new Vector2();
    AnimatedSprite sprite;

    public override void _Ready()
    {
        sprite = GetNode("AnimatedSprite") as AnimatedSprite;
    }

    public Vector2 CartesianToIsometric(Vector2 cartesian) {
        return new Vector2(
            cartesian.x - cartesian.y,
            (cartesian.x + cartesian.y) / 2
        );
    }

    public override void _Process(float delta)
    {
        Vector2 direction = new Vector2();

        // Note: right now only 4-direction movement is implemented because 
        // we don't have sprites for 8-directional and it looks confusing
        // to re-add 8-direction, simlpy make all of these "+="
        if (Godot.Input.IsActionPressed("player_up")) {
            direction = Vector2.Up; // (0, -1)
            sprite.Animation = "NE";
        } else if (Godot.Input.IsActionPressed("player_down")) {
            direction = Vector2.Down; // (0, 1)
            sprite.Animation = "SW";
        }

        if (Godot.Input.IsActionPressed("player_left")) {
            direction = Vector2.Left; // (-1, 0)
            sprite.Animation = "NW";
        } else if (Godot.Input.IsActionPressed("player_right")) {
            direction = Vector2.Right; // (1, 0)
            sprite.Animation = "SE";
        }

        motion = direction.Normalized() * SPEED * delta;
        motion = CartesianToIsometric(motion);

        MoveAndCollide(motion);
    }
}