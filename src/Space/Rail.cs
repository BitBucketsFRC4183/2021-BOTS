using Godot;
using System;

public class Rail : SpaceProjectile, SpaceDamagable
{
    public bool PartialHoming = false;
    public float Acceleration = 100f;
    public float rotationDeadband = 0.3f;
    public float rotationSpeed = 30f;
    public Node2D Target;
    public void Hit()
    {
        Destroyed = true;
        QueueFree();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if (PartialHoming)
        {
            float targetAngle = GetAngleTo(Target.GlobalPosition);
            if (Mathf.Abs(targetAngle) >= rotationDeadband)
            {
                if (targetAngle > 0)
                {
                    Rotate(-rotationSpeed * delta);
                }
                else
                {
                    Rotate(rotationSpeed * delta);
                }
            }
            AddForce(Rotation, Acceleration * delta);

        }
    }
}
