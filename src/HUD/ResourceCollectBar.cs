using Godot;

public class ResourceCollectBar : Control {
    TextureProgress progress;
    Tween tween;

    public override void _Ready() {
        progress = GetNode<TextureProgress>("TextureProgress");
        tween = GetNode<Tween>("Tween");

        Signals.Instance.Connect("CollectingResource", this, nameof(OnCollectingResource));
        Signals.Instance.Connect("NotCollectingResource", this, nameof(OnNotCollectingResource));
        this.Hide();
    }

    void OnCollectingResource() {
        this.Show();
        tween.InterpolateProperty(progress, "value", 0, 100, 2);
        tween.Start();
    }

    void OnNotCollectingResource() {
        this.Hide();
        tween.Stop(progress);
    }

}