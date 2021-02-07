using Godot;
using System;

public class InfoPanel : Control
{
    public UpgradeCost cost { get; set; }

    public override void _Ready()
    {
        for (int i = 0; i < UpgradeCost.NumResourceTypes; i++)
        {
            var bar = GetNode<Panel>("Panel").GetNode<VBoxContainer>("Vbox").GetNode<ProgressBar>("Resource" + (i + 1));

            if (cost.Costs[i] == 0) bar.Visible = false;
            else bar.MaxValue = cost.Costs[i];
        }
    }
}
