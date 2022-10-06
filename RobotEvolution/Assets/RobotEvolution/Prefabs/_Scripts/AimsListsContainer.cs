using System.Collections.Generic;
using UnityEngine;

public class AimsListsContainer : MonoBehaviour
{
    [SerializeField] private PoolRandomPoints _poolRandomPoints;
    [SerializeField] private PoolSimpleBots _poolSimpleBots;
    [SerializeField] private PoolPlayer _poolPlayer;
    [SerializeField] private PoolGear _poolGear;
    [SerializeField] private PoolBattery _poolBattery;

    private DistanceToAimComparer _distanceToAimComparer;
    private List<RandomPoints> _randomPointsList;
    private List<IDistanceAimsComparable> _enemysList = new();
    private List<IDistanceAimsComparable> _gearsList = new();
    private List<IDistanceAimsComparable> _batteryList = new();


    private void Awake()
    {
        _distanceToAimComparer = new();
    }

    private void Start()
    {
        SetRandomPointsList();
        SetComparableGearsLists();
        SetComparableBatteryLists();
        SetComparableEnemysList();
    }

    #region Get aims lists

    public List<RandomPoints> GetRandomPointsList()
    {
        return _randomPointsList;
    }
    public List<IDistanceAimsComparable> GetEnemysSortedList(Transform characterTransform)
    {
        _enemysList.Clear();

        SetComparableEnemysList();

        _enemysList = SortingAimsList(_enemysList, characterTransform);

        _enemysList.RemoveAt(0);

        return _enemysList;
    }
    #endregion

    #region Set aims lists
    private void SetRandomPointsList()
    {
        _randomPointsList = _poolRandomPoints.WholeRandomPointsList;
    }

    private void SetComparableGearsLists()
    {
        _gearsList = CastAimsListsToComparableInterface(_poolGear.WholeGearsList);
    }

    private void SetComparableBatteryLists()
    {
        _batteryList = CastAimsListsToComparableInterface(_poolBattery.WholeBatteryList);
    }

    private void SetComparableEnemysList()
    {
        _enemysList.AddRange(SetSimpleBotsList());
        _enemysList.AddRange(SetComapablePlayerLisys());
    }
    private List<IDistanceAimsComparable> SetSimpleBotsList()
    {
        return CastAimsListsToComparableInterface(_poolSimpleBots.WholeSimpleBotList);
    }
    private List<IDistanceAimsComparable> SetComapablePlayerLisys()
    {
        return CastAimsListsToComparableInterface(_poolPlayer.WholePlayerList);
    }
    #endregion


    private List<IDistanceAimsComparable> SortingAimsList(List<IDistanceAimsComparable> aimsList, Transform characterTransform)
    {
        foreach (IDistanceAimsComparable item in aimsList)
        {
            item.CalculateDistanceAimToCharacter(characterTransform);
        }

        aimsList.Sort(_distanceToAimComparer);

        return aimsList;
    }

    private List<IDistanceAimsComparable> CastAimsListsToComparableInterface<T>(List<T> aimsList) where T: IDistanceAimsComparable
    {
        List<IDistanceAimsComparable> castedAimsList = new();
        foreach (var item in aimsList)
        {
            castedAimsList.Add(item);
        }
        return castedAimsList;
    }
}
