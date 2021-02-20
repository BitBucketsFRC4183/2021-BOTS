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
    public float LaserCooldown = 0.25f;
    [Export]
    public float RailgunCooldown = 1;
    [Export]
    public float MissileCooldown = 0.5f;
    [Export]
    public int ShieldStrength;
    [Export]
    public bool MissileHoming = false;
    [Export]
    public bool RailHoming = false;

    public float RotationDeadband = 0.3f;

    public float ProjectileEjectionForce = 10;
    public float RailEjectionForce = 750;

    public float MissileOffset = 16;

    private float targetLockRadius = 100;

    public Destroyable WeaponTarget = null;

    public bool CanLaserFire = true;
    public bool CanRailgunFire = true;
    public bool CanMissileFire = true;

    [Export]
    public PackedScene MissileScene;
    [Export]
    public PackedScene RailScene;
    private Sprite targetLockIndicator;
    private Sprite thrusterFlame;
    private Camera2D activeCamera;
    private Timer laserCooldownTimer;
    private Timer railgunCooldownTimer;
    private Timer missileCooldownTimer;

    public event Action Destroyed;

    public event Action Destroyed;

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
        activeCamera = GetNode<Camera2D>("ShipCam");
        thrusterFlame = GetNode<Sprite>("ThrusterFlame");
        laserCooldownTimer = GetNode<Timer>("LaserCooldown");
        railgunCooldownTimer = GetNode<Timer>("RailgunCooldown");
        missileCooldownTimer = GetNode<Timer>("MissileCooldown");

        laserCooldownTimer.Connect("timeout", this, "OnLaserCooldownFinished");
        railgunCooldownTimer.Connect("timeout", this, "OnRailgunCooldownFinished");
        missileCooldownTimer.Connect("timeout", this, "OnMissileCooldownFinished");
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
                thrusterFlame.Visible = true;
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
                Rotation = -velocityAngle;
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
        if (Input.IsActionJustPressed("ship_land"))
        {
            Godot.Collections.Array landables = GetTree().GetNodesInGroup("Landables");
            TryLand(landables);
        }
        if (Input.IsActionJustReleased("player_up"))
        {
            thrusterFlame.Visible = false;
        }
    }
    public void FireWeapon(Destroyable target, Weapon weapon)
    {
        if (weapon == Weapon.Missile && CanMissileFire)
        {
            Missile missile1 = (Missile)MissileScene.Instance();
            missile1.Homing = MissileHoming;
            missile1.Target = target;
            missile1.GlobalRotation = GlobalRotation;
            missile1.Position = GlobalPosition;
            missile1.Position += new Vector2(MissileOffset, 0).Rotated(GlobalRotation + Mathf.Deg2Rad(90));
            missile1.Velocity = Velocity;
            missile1.AddForce(new Vector2(1, 0).Rotated(GlobalRotation + Mathf.Deg2Rad(90)), ProjectileEjectionForce);
            GetParent().AddChild(missile1);

            Missile missile2 = (Missile)MissileScene.Instance();
            missile2.Homing = MissileHoming;
            missile2.Target = target;
            missile2.GlobalRotation = GlobalRotation;
            missile2.Position = GlobalPosition;
            missile2.Position += new Vector2(MissileOffset, 0).Rotated(GlobalRotation + Mathf.Deg2Rad(-90));
            missile2.Velocity = Velocity;
            missile2.AddForce(new Vector2(1, 0).Rotated(GlobalRotation + Mathf.Deg2Rad(-90)), ProjectileEjectionForce);
            GetParent().AddChild(missile2);

            CanMissileFire = false;
            missileCooldownTimer.Start(MissileCooldown);
        }
        else if (weapon == Weapon.Laser && CanLaserFire)
        {
            if (target is SpaceDamagable damagable)
            {
                damagable.Hit();
                CanLaserFire = false;
                laserCooldownTimer.Start(LaserCooldown);
            }
        }
        else if (weapon == Weapon.Railgun && CanRailgunFire)
        {

            Rail rail = (Rail)RailScene.Instance();
            rail.PartialHoming = RailHoming;
            rail.Velocity = Velocity;
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
            rail.Position = GlobalPosition;
            GetParent().AddChild(rail);

            CanRailgunFire = false;
            railgunCooldownTimer.Start(RailgunCooldown);
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
            Destroyed?.Invoke();
            QueueFree();
        }
    }



    public override void OnCollision(Node2D body)
    {
        Hit();
    }

    public void TryLock(Godot.Collections.Array lockables)
    {
        Vector2 distance = Vector2.Inf;
        Destroyable tempTarget = null;

        foreach (Destroyable lockable in lockables)
        {
            Vector2 lockableDistance = lockable.GlobalPosition - GetGlobalMousePosition();
            lockableDistance.x = Mathf.Abs(lockableDistance.x);
            lockableDistance.y = Mathf.Abs(lockableDistance.y);

            if (lockableDistance < distance)
            {
                distance = lockableDistance;
                tempTarget = lockable;
            }
        }
        if (Mathf.Abs(distance.x) > targetLockRadius * activeCamera.Zoom.x || Mathf.Abs(distance.y) > targetLockRadius * activeCamera.Zoom.y)
        {
            if (WeaponTarget != null)
            {
                WeaponTarget.Destroyed -= OnTargetDestroyed;
            }
            WeaponTarget = null;
        }
        else
        {
            WeaponTarget = tempTarget;
            WeaponTarget.Destroyed += OnTargetDestroyed;
        }
    }

    public void TryLand(Godot.Collections.Array landables)
    {
        Vector2 distance = Vector2.Inf;
        LandableThing tempLandable = null;

        foreach (LandableThing landable in landables)
        {
            Vector2 landableDistance = landable.GlobalPosition - GlobalPosition;
            landableDistance.x = Mathf.Abs(landableDistance.x);
            landableDistance.y = Mathf.Abs(landableDistance.y);

            if (landableDistance < distance)
            {
                distance = landableDistance;
                tempLandable = landable;
            }
        }
        if (Mathf.Abs(distance.x) > tempLandable.LandableRadius || Mathf.Abs(distance.y) > tempLandable.LandableRadius)
        {
            tempLandable.Land();
        }
        else
        {

        }
    }

    private void OnTargetDestroyed()
    {
        WeaponTarget = null;
    }

    public void OnLaserCooldownFinished()
    {
        CanLaserFire = true;
    }

    public void OnRailgunCooldownFinished()
    {
        CanRailgunFire = true;
    }

    public void OnMissileCooldownFinished()
    {
        CanMissileFire = true;
    }
}
