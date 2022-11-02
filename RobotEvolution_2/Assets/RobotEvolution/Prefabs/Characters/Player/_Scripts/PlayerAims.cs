using System.Collections.Generic;

public class PlayerAims : CharactersAims
{
    public List<IDistanceAimsComparable> GetNearestEnemyListForIndicationArrow(int amountAimsRequest)
    {
        return GetPathComplitedSortAimList(amountAimsRequest, _aimsListsContainer.GetEnemysSortedList(_thisTransform));
    }
}
