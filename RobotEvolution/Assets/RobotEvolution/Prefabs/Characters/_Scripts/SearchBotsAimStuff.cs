using UnityEngine;

public class SearchBotsAimStuff :AbsSearcBotshAim
{
    [SerializeField] private PoolGear _poolGear;

    public override void SelectSearcingAimLists()
    {
        SearchNearestAimInList<Gear>(_poolGear.WholeGearsList);
    }

}
