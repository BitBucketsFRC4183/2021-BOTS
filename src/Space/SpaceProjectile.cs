using Godot;
using System;

public class SpaceProjectile : SpacePhysicsObject, Destroyable
{
    [Export]
    public float Lifetime = 60;
    public event Action Destroyed;

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Lifetime -= delta;
        if (Lifetime <= 0)
        {
            Destroy();
        }
    }
    public void Destroy()
    {
        Destroyed?.Invoke();
        QueueFree();
    }
}
