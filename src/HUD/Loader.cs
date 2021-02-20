using Godot;
using System.Threading.Tasks;

public class Loader : Control
{
    ResourceInteractiveLoader loader;
    TextureProgress progress;
    Tween tween;

    Node Root;
    Node currentScene;

    public override void _Ready()
    {
        Visible = false;

        progress = GetNode<TextureProgress>("TextureProgress");
        Root = GetTree().Root;

        tween = new Tween();

        currentScene = Root.GetChild(Root.GetChildCount() - 1);
    }

    public void loadPlanet(string filePath)
    {
        Visible = true;
        loader = ResourceLoader.LoadInteractive(filePath);
    }

    public override void _Process(float delta)
    {
        if (loader == null)
        {
            return;
        }

        Error err = loader.Poll();

        if (err == Error.FileEof)
        {
            Resource resource = loader.GetResource();
            loader = null;
            setNewScene((PackedScene)resource);
            return;
        }
        else if (err == Error.Ok)
        {
            updateProgress();
            return;
        }
    }

    public void updateProgress()
    {
        var loadProgress = ((float)loader.GetStage()) / loader.GetStageCount();

        progress.Value = loadProgress * 100;
    }

    async public Task setNewScene(PackedScene resource)
    {
        await SceneChanger.Instance.Fade();

        currentScene.QueueFree();

        currentScene = resource.Instance();
        Root.AddChild(currentScene);

        await SceneChanger.Instance.ReverseFade();
    }

}
