using Godot;
using System;

public class Rail : SpaceProjectile, SpaceDamagable
{
    public bool PartialHoming = false;
    public float CruiseVelocity = 1000f;
    public float Acceleration = 100f;
    public float RotationDeadband = 0.1f;
    public float RotationSpeed = 0.3f;
    public Destroyable Target;

    public override void _Ready()
    {
        base._Ready();
        if (Target != null)
        {
            Target.Destroyed += OnTargetDestroyed;
        }
    }
    public void Hit()
    {
        Destroy();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if (PartialHoming)
        {
            if (Target != null)
            {
                float targetAngle = GetAngleTo(Target.GlobalPosition);
                if (Mathf.Abs(targetAngle) >= RotationDeadband)
                {
                    if (targetAngle > 0)
                    {
                        Rotate(RotationSpeed * delta);
                    }
                    else
                    {
                        Rotate(-RotationSpeed * delta);
                    }
                }
                else
                {
                    Rotate(targetAngle);
                }
            }
            AddForce(Rotation, Acceleration * delta);
        }
    }
    public override void OnCollision(Node2D body)
    {
        Hit();
    }
    private void OnTargetDestroyed()
    {
        Target = null;
    }
}
