using Godot;

public class GameResource : Area2D
{
    [Export] public Enums.GameResources type = Enums.GameResources.Graprofium;
    [Export] public int amount = 0;

    void _on_GameResource_body_entered(Node body) {
        if (body is Player) {
            Signals.Instance.EmitSignal(nameof(Signals.OverlappingResource), this);
        }
    }

    void _on_GameResource_body_exited(Node body) {
        if (body is Player) {
            Signals.Instance.EmitSignal(nameof(Signals.NotOverlappingResource));
        }
    }
}
