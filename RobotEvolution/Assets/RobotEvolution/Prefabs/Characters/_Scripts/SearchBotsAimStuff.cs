using UnityEngine;

public class SearchBotsAimStuff :AbsSearcBotshAim
{
    [SerializeField]private PoolGear _poolGear;
    [SerializeField]private PoolBattery _poolBattery;

    public override void SelectSearcingAimLists()
    {
        SearchNearestAimInList<Gear>(_poolGear.WholeGearsList);
        SearchNearestAimInList<Battery>(_poolBattery.WholeBatteryList);
    }

}
