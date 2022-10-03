using System.Collections.Generic;
using UnityEngine;

public class SearchBotsAimEnemy : AbsSearcBotshAim
{
    [SerializeField] private PoolPlayer _poolPlayer;
    [SerializeField] private PoolSimpleBots _poolSimpleBots;

    private DistanceToAimComparer _distanceToAimComparer;
    private List<IDistanceAimsComparable> _quikSortEnemyList = new();

    private void Awake()
    {
        _distanceToAimComparer = new DistanceToAimComparer();
    }

    public override void SetectListForMutualAimsList()
    {
        CreateMutualQuikSortAimsList(_poolPlayer.WholePlayerList);
        CreateMutualQuikSortAimsList(_poolSimpleBots.WholeSimpleBotList);
    }

    public override void CreateMutualQuikSortAimsList<T>(List<T> List)
    {
        foreach (IDistanceAimsComparable item in List)
        {
            _quikSortEnemyList.Add(item);
        }   
    }

    public override List<IDistanceAimsComparable> GetSortFoDistanceAimsList(Transform characterTransform)
    {
        _quikSortEnemyList.Clear();

        SetectListForMutualAimsList();

        foreach (IDistanceAimsComparable item in _quikSortEnemyList)
        {
            item.CalculateDistanceAimToCharacter(characterTransform);
        }

        _quikSortEnemyList.Sort(_distanceToAimComparer);

        _quikSortEnemyList.RemoveAt(0); // Remove zero distance, compare by myself

        return _quikSortEnemyList;
    }
}
