public class RoverFuelData
{
    public int CurrentFuel { get; set; }
    public int MaxFuel { get; set; }

    public RoverFuelData()
    {
        Signals.UpgradesChanged += UpdateMaxFuel;
        
        UpdateMaxFuel();
        CurrentFuel = MaxFuel;
    }

    private void UpdateMaxFuel()
    {
        MaxFuel = (PlayerData.Instance.roverLevels[Enums.RoverUpgradeType.TANK] + 1) * 200;
        CurrentFuel = MaxFuel;
    }

    public void UseFuel()
    {
        CurrentFuel--;
        if (CurrentFuel <= 0) CurrentFuel = 0;
    }

    public void RegenFuel()
    {
        CurrentFuel += (PlayerData.Instance.roverLevels[Enums.RoverUpgradeType.REGEN] + 1) * 2;
        if (CurrentFuel >= MaxFuel) CurrentFuel = MaxFuel;
    }
}