using Godot;
using System;

public class CelestialBody : KinematicBody2D
{
    [Export]
    public float Gravity = 9.8f;
    [Export]
    public Node2D OrbitalParent = null;

    public Area2D AreaOfInfluence;
    private Godot.Collections.Array InfluencedBodies;
    public override void _Ready()
    {
        AreaOfInfluence = GetNode<Area2D>("AreaOfInfluence");
        AreaOfInfluence.Connect("body_entered", this, "UpdateInfluence");
        CollisionShape2D AoICollision = GetNode<CollisionShape2D>("AreaOfInfluence/CollisionShape2D");

        CircleShape2D AoIShape = new CircleShape2D();
        AoIShape.Radius = Gravity * 1000;

        AoICollision.Shape = AoIShape;

        InfluencedBodies = AreaOfInfluence.GetOverlappingBodies();
        CallDeferred("UpdateInfluence", this);
    }

    public override void _PhysicsProcess(float delta)
    {
        foreach (Node node in InfluencedBodies)
        {
            if (node is SpacePhysicsObject body)
            {
                float distanceFromBody = GlobalPosition.DistanceTo(body.GlobalPosition);

                body.AddForce(body.GetAngleTo(GlobalPosition) + body.Rotation, Gravity / (distanceFromBody / (Gravity * 100)) * delta);
            }
        }
        if (OrbitalParent != null)
        {

        }
    }
    public void UpdateInfluence(Node body)
    {
        InfluencedBodies = AreaOfInfluence.GetOverlappingBodies();
    }
}
