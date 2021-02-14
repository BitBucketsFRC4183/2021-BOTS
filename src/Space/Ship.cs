using Godot;
using System;

public class Ship : SpacePhysicsObject, SpaceDamagable
{
    [Export]
    public float Fuel = 100;
    [Export]
    public float MaxFuel = 100;
    [Export]
    public float Speed = 50;
    [Export]
    public float RotationSpeed = 1f;
    [Export]
    public int ShieldStrength;
    [Export]
    public bool MissileHoming = false;
    [Export]
    public bool RailHoming = false;

    public float RotationDeadband = 0.3f;

    public float ProjectileEjectionForce = 10;
    public float RailEjectionForce = 100;

    public float MissileOffset = 16;

    private float targetLockRadius = 100;

    public Node2D WeaponTarget = null;


    public bool CanLaserFire = true;

    [Export]
    public PackedScene MissileScene;
    [Export]
    public PackedScene RailScene;
    private Sprite targetLockIndicator;
    private Camera2D ActiveCamera;

    public enum Weapon
    {
        Missile,
        Laser,
        Railgun
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        targetLockIndicator = GetNode<Sprite>("LockIndicator");
        ActiveCamera = GetNode<Camera2D>("ShipCam");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (WeaponTarget is null)
        {
            targetLockIndicator.Visible = false;
            targetLockIndicator.GlobalPosition = GlobalPosition;
        }
        else
        {
            targetLockIndicator.Visible = true;
            targetLockIndicator.GlobalPosition = WeaponTarget.GlobalPosition;
        }
        // Placeholder movement code
        if (Input.IsActionPressed("player_up"))
        {
            if (Fuel > 0)
            {
                AddForce(GlobalRotation, Speed * delta);
                Fuel -= delta;
            }
        }
        // Keep the idea of this though, pressing S makes the ship try to face the opposite direction of it's velocity.
        if (Input.IsActionPressed("player_down"))
        {
            float velocityAngle = -GetAngleTo(Velocity + GlobalPosition);
            if (Mathf.Abs(velocityAngle) >= RotationDeadband)
            {
                if (velocityAngle > 0)
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
                Rotate(velocityAngle);
            }
        }
        if (Input.IsActionPressed("player_left"))
        {
            Rotation -= (RotationSpeed * delta);
        }
        if (Input.IsActionPressed("player_right"))
        {
            Rotation += (RotationSpeed * delta);
        }
    }

    public override void _Input(InputEvent @event)
    {
        float delta = GetProcessDeltaTime();
        if (Input.IsActionJustPressed("ship_fire_primary"))
        {
            if (WeaponTarget != null)
            {
                FireWeapon(WeaponTarget, Weapon.Railgun);
            }
            else
            {
                FireWeapon(null, Weapon.Railgun);
            }
        }
        else if (Input.IsActionJustPressed("ship_fire_secondary"))
        {
            if (WeaponTarget != null)
            {
                FireWeapon(WeaponTarget, Weapon.Missile);
            }
            else
            {

            }
        }
        if (Input.IsActionJustPressed("ship_target_lock"))
        {
            Godot.Collections.Array lockables = GetTree().GetNodesInGroup("Lockables");
            TryLock(lockables);
        }
    }
    public void FireWeapon(Node2D target, Weapon weapon)
    {
        if (weapon == Weapon.Missile)
        {
            Missile missile1 = (Missile)MissileScene.Instance();
            missile1.Homing = MissileHoming;
            missile1.Target = target;
            missile1.GlobalRotation = GlobalRotation;
            missile1.GlobalPosition = GlobalPosition;
            missile1.GlobalPosition += new Vector2(MissileOffset, 0).Rotated(GlobalRotation + Mathf.Deg2Rad(90));
            missile1.AddForce(new Vector2(1, 0).Rotated(GlobalRotation + Mathf.Deg2Rad(90)), ProjectileEjectionForce);
            GetParent().AddChild(missile1);

            Missile missile2 = (Missile)MissileScene.Instance();
            missile2.Homing = MissileHoming;
            missile2.Target = target;
            missile2.GlobalRotation = GlobalRotation;
            missile2.GlobalPosition = GlobalPosition;
            missile2.GlobalPosition += new Vector2(MissileOffset, 0).Rotated(GlobalRotation + Mathf.Deg2Rad(-90));
            missile2.AddForce(new Vector2(1, 0).Rotated(GlobalRotation + Mathf.Deg2Rad(-90)), ProjectileEjectionForce);
            GetParent().AddChild(missile2);
        }
        else if (weapon == Weapon.Laser)
        {
            if (target is SpaceDamagable damagable)
            {
                damagable.Hit();
                CanLaserFire = false;
            }
        }
        else if (weapon == Weapon.Railgun)
        {

            Rail rail = (Rail)RailScene.Instance();
            rail.PartialHoming = RailHoming;
            if (target != null)
            {
                rail.Target = target;
                rail.GlobalRotation = GlobalRotation + GetAngleTo(GetGlobalMousePosition());
                rail.AddForce(new Vector2(1, 0).Rotated(GlobalRotation + GetAngleTo(GetGlobalMousePosition())), RailEjectionForce);
            }
            else
            {
                rail.GlobalRotation = GlobalRotation + GetAngleTo(GetGlobalMousePosition());
                rail.AddForce(new Vector2(1, 0).Rotated(GlobalRotation + GetAngleTo(GetGlobalMousePosition())), RailEjectionForce);
            }
            rail.GlobalPosition = GlobalPosition;
            GetParent().AddChild(rail);
        }
    }

    public void Hit()
    {
        if (ShieldStrength > 0)
        {
            ShieldStrength -= 1;
        }
        else
        {
            Destroyed = true;
            QueueFree();
        }
    }

    public void TryLock(Godot.Collections.Array lockables)
    {
        Vector2 distance = Vector2.Inf;
        Node2D tempTarget = null;

        foreach (Node2D lockable in lockables)
        {
            Vector2 lockableDistance = lockable.GlobalPosition - GetGlobalMousePosition();
            lockableDistance.x = Mathf.Abs(lockableDistance.x);
            lockableDistance.y = Mathf.Abs(lockableDistance.y);

            if (lockableDistance < distance)
            {
                distance = lockable.GlobalPosition - GetGlobalMousePosition();
                tempTarget = lockable;
            }
        }
        if (Mathf.Abs(distance.x) > targetLockRadius * ActiveCamera.Zoom.x || Mathf.Abs(distance.y) > targetLockRadius * ActiveCamera.Zoom.y)
        {
            WeaponTarget = null;
        }
        else
        {
            WeaponTarget = tempTarget;
        }
    }
}
