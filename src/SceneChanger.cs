using Godot;
using System;
using System.Threading.Tasks;

public class SceneChanger : CanvasLayer
{
    public static SceneChanger Instance { get; private set; }
    
    AnimationPlayer player;

    public override void _Ready()
    {
        player = GetNode<AnimationPlayer>("AnimationPlayer");
        Instance = this;
    }

    async public Task Fade() {
        player.Play("fade");
        await ToSignal(player, "animation_finished");
    }

    async public Task ReverseFade() {
        player.Play("reverse fade");
        await ToSignal(player, "animation_finished");
    }

    async public void GoToScene(PackedScene scene) {
        await Fade();

        GetTree().ChangeSceneTo(scene);

        await ReverseFade();
    }

}
