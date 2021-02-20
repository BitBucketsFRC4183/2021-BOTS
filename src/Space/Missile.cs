using Godot;
using System;

public class Missile : SpaceProjectile, SpaceDamagable, Destroyable
{
    public bool Homing = false;
    public float CruiseVelocity = 10000f;
    public float Acceleration = 100f;
    public float RotationDeadband = 0.1f;
    public float RotationSpeed = 4f;
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
        if (Homing)
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

            Vector2 targetVelocityDifference;
            if (Target is SpacePhysicsObject body)
            {
                targetVelocityDifference = body.Velocity - Velocity;
            }
            else
            {
                targetVelocityDifference = -Velocity;
            }
            targetVelocityDifference = targetVelocityDifference.Normalized() / 2;
            AddForce(new Vector2(1, 0).Rotated(Rotation) + targetVelocityDifference, Acceleration * delta);

            Lifetime -= delta;
        }
        else
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
