using Godot;
using System;

public class InfoPanel : Control
{
    [Export] public String upgradeName;
    
    public UpgradeCost cost { get; set; }

    public override void _Ready()
    {
        //Update progress bars here
    }
}
