using Godot;
using System;

public class CelestialBody : KinematicBody2D
{
    [Export]
    public float Gravity = 9.8f;
    [Export]
    public float OrbitalRotationSpeed = 1;
    [Export]
    public string OrbitalParentPath;
    public CelestialBody OrbitalParent = null;

    public Area2D AreaOfInfluence;
    private Godot.Collections.Array influencedBodies;

    public override void _Ready()
    {
        AreaOfInfluence = GetNode<Area2D>("AreaOfInfluence");
        AreaOfInfluence.Connect("body_entered", this, "UpdateInfluence");
        CollisionShape2D AoICollision = GetNode<CollisionShape2D>("AreaOfInfluence/CollisionShape2D");

        CircleShape2D AoIShape = new CircleShape2D();
        AoIShape.Radius = Gravity * 1000;

        AoICollision.Shape = AoIShape;

        if (OrbitalParentPath != null)
        {
            OrbitalParent = (CelestialBody)GetTree().Root.GetNode<CelestialBody>(OrbitalParentPath);
        }

        influencedBodies = AreaOfInfluence.GetOverlappingBodies();
        CallDeferred("UpdateInfluence", this);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (influencedBodies != null)
        {
            foreach (Node node in influencedBodies)
            {
                if (node is SpacePhysicsObject body)
                {
                    float distanceFromBody = GlobalPosition.DistanceTo(body.GlobalPosition);

                    body.AddForce(body.GetAngleTo(GlobalPosition) + body.Rotation, Gravity / (distanceFromBody / (Gravity * 100)) * delta);
                }
            }
        }
        if (OrbitalParent != null)
        {
            Vector2 vectorToParent = GlobalPosition - OrbitalParent.GlobalPosition;
            float distanceToParent = GlobalPosition.DistanceTo(OrbitalParent.GlobalPosition);
            GlobalPosition = OrbitalParent.GlobalPosition + (GlobalPosition - OrbitalParent.GlobalPosition).Rotated(OrbitalRotationSpeed * delta);
        }
    }

    public void UpdateInfluence(Node body)
    {
        influencedBodies = AreaOfInfluence.GetOverlappingBodies();
    }
}
