using Godot;
using System;

public class UpgradeCost : Node
{
    public static int NumResourceTypes = 6;
    public int[] Costs { get; set; }

    public UpgradeCost(int g = 0, int w = 0, int k = 0, int e = 0, int x = 0, int c = 0)
    {
        Costs = new[] {g, w, k, e, x, c};
    }

    public UpgradeCost() : this(0) {}
}
