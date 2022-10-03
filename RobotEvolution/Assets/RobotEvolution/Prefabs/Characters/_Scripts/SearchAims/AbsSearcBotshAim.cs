using System.Collections.Generic;
using UnityEngine;

public abstract class AbsSearcBotshAim : MonoBehaviour
{
    public abstract void SetectListForMutualAimsList();

    public abstract void CreateMutualQuikSortAimsList<T>(List<T> List) where T : IDistanceAimsComparable;

    public abstract List<IDistanceAimsComparable> GetSortFoDistanceAimsList(Transform characterTransform);
}
