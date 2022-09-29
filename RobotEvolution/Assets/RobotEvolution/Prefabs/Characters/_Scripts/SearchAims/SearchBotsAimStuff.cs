using System.Collections.Generic;
using UnityEngine;

public class SearchBotsAimStuff :AbsSearcBotshAim
{
    [SerializeField]private PoolGear _poolGear;
    [SerializeField]private PoolBattery _poolBattery;

    private DistanceToAimComparer _distanceToAimComparer;
    private List<IDistanceToAimQuikSortable> _quikSortStuffList = new();

    private void Awake()
    {
        _distanceToAimComparer = new DistanceToAimComparer();
    }

    public override void SetectListForMutualAimsList() // TODO: Make selection lists thet the character needs now
    {
        CreateMutualQuikSortAimsList(_poolGear.WholeGearsList);
        CreateMutualQuikSortAimsList(_poolBattery.WholeBatteryList);
    }

    public override void CreateMutualQuikSortAimsList<T>(List<T> List)
    {
        foreach (IDistanceToAimQuikSortable item in List)
        {
            _quikSortStuffList.Add(item);
        }
    }

    public override List<IDistanceToAimQuikSortable> GetSortFoDistanceAimsList(Transform characterTransform)
    {
        _quikSortStuffList.Clear();

        SetectListForMutualAimsList();

        foreach (IDistanceToAimQuikSortable item in _quikSortStuffList)
        {
            item.CalculateDistanceAimToCharacter(characterTransform);
        }

        _quikSortStuffList.Sort(_distanceToAimComparer);

        return _quikSortStuffList;
    }
}
