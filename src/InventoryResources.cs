using System;
using static Enums;

public class InventoryResources {

    public int Graprofium { get; set; } = 0;
    public int Kamenium { get; set; } = 0;
    public int Wooflowium { get; set; } = 0;
    public int Efarcium { get; set; } = 0;
    public int Xerocrium { get; set; } = 0;
    public int Coopertonium { get; set; } = 0;
    
    // overload get and set
    public int this[GameResources index] {
        get {
            switch (index) {
                case GameResources.Graprofium:
                    return Graprofium;
                case GameResources.Kamenium:
                    return Kamenium;
                case GameResources.Wooflowium:
                    return Wooflowium;
                case GameResources.Efarcium:
                    return Efarcium;
                case GameResources.Xerocrium:
                    return Xerocrium;
                case GameResources.Coopertonium:
                    return Coopertonium;
                default:
                    throw new IndexOutOfRangeException($"Index {index} out of range for {this.GetType().ToString()}");
            }
        }

        set {
            switch (index) {
                case GameResources.Graprofium:
                    Graprofium = value;
                    break;
                case GameResources.Kamenium:
                    Kamenium = value;
                    break;
                case GameResources.Wooflowium:
                    Wooflowium = value;
                    break;
                case GameResources.Efarcium:
                    Efarcium = value;
                    break;
                case GameResources.Xerocrium:
                    Xerocrium = value;
                    break;
                case GameResources.Coopertonium:
                    Coopertonium = value;
                    break;
                default:
                    throw new IndexOutOfRangeException($"Index {index} out of range for {this.GetType().ToString()}");
            }
        }
    }
}
