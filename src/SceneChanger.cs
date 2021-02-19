using Godot;
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

        // check if the planet scene is loaded in and remove if it is
        // because we add it in manually, we need to remove it manually
        var planetSceneNodes = GetTree().GetNodesInGroup("planet lands");
        if (planetSceneNodes.Count > 0) {
            Node node = (Node) planetSceneNodes[0];
            node.QueueFree();
        }

        GetTree().ChangeSceneTo(scene);

        await ReverseFade();
    }

}
