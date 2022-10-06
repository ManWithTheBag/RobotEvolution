using System.Collections.Generic;
using UnityEngine;

public class PlayerAims : CharactersAims
{
    public override void Start()
    {
        base.Start();
    }

    public List<IDistanceAimsComparable> GetNearestEnemyListForIndicationArrow(int amountAimsRequest)
    {
        return GetPathComplitedSortAimList(amountAimsRequest, _aimsListsContainer.GetEnemysSortedList(_thisTransform));
    }
}
