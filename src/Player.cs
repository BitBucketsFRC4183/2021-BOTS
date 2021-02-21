using Godot;
using System;

public class Player : KinematicBody2D
{
    private int SPEED = 400;
    Vector2 motion = new Vector2();
    
    AnimationPlayer animPlayer;
    Timer timer;

    bool overlappingResource = false;
    GameResource resource;

    public override void _Ready()
    {
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        timer = GetNode<Timer>("ResourceCollectTimer");
        
        Signals.Instance.Connect("OverlappingResource", this, nameof(OnOverlappingResource));
        Signals.Instance.Connect("NotOverlappingResource", this, nameof(OnNotOverlappingResource));

        Signals.UpgradesChanged += UpdateSpeed;
    }

    public override void _ExitTree()
    {
        Signals.UpgradesChanged -= UpdateSpeed;
    }

    private void UpdateSpeed()
    {
        int baseSpeed = 400;
        switch (PlayerData.Instance.roverLevels[Enums.RoverUpgradeType.SPEED])
        {
            case 1: SPEED = 450;
                break;
            case 2: SPEED = 500;
                break;
            case 3: SPEED = 600;
                break;
            default: SPEED = baseSpeed;
                break;
        }
    }

    /////////
    // collecting resources code

    void OnOverlappingResource(GameResource res) {
        overlappingResource = true;
        resource = res;
    }
    void OnNotOverlappingResource() {
        overlappingResource = false;
        GD.Print("stopped collecting");
        resource = null;
        timer.Stop();
        Signals.Instance.EmitSignal(nameof(Signals.NotCollectingResource));
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("resource_collect")) {
            if (overlappingResource) {
                timer.Start();
                Signals.Instance.EmitSignal(nameof(Signals.CollectingResource));
            }
        }

        if (@event.IsActionReleased("resource_collect")) {
            GD.Print("stopped collecting");
            resource = null;
            timer.Stop();
            Signals.Instance.EmitSignal(nameof(Signals.NotCollectingResource));
            
        }
    }

    void _on_ResourceCollectTimer_timeout() {
        GD.Print("finished!");
        if (resource != null) {
            PlayerData.Instance.resources[resource.type] += resource.amount;
            resource.QueueFree();
            resource = null;
            Signals.Instance.EmitSignal(nameof(Signals.NotCollectingResource));
            
            Signals.PublishUpgradesChangedEvent();
        }
    }
    
    
    /////////
    // movement code

    Vector2 CartesianToIsometric(Vector2 cartesian) {
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
        if (Input.IsActionPressed("player_up")) {
            direction = Vector2.Up; // (0, -1)
            animPlayer.Play("NE");
        } else if (Input.IsActionPressed("player_down")) {
            direction = Vector2.Down; // (0, 1)
            animPlayer.Play("SW");
        }

        if (Input.IsActionPressed("player_left")) {
            direction = Vector2.Left; // (-1, 0)
            animPlayer.Play("NW");
        } else if (Input.IsActionPressed("player_right")) {
            direction = Vector2.Right; // (1, 0)
            animPlayer.Play("SE");
        }

        motion = direction.Normalized() * SPEED * delta;
        motion = CartesianToIsometric(motion);

        MoveAndCollide(motion);

        Position = new Vector2(
                        Mathf.Clamp(Position.x, 0, GetViewportRect().Size.x),
                        Mathf.Clamp(Position.y, 0, GetViewportRect().Size.y)
                    );
    }
}