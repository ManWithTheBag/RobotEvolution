using System.Collections.Generic;
using UnityEngine;

public abstract class AbsSearcBotshAim : MonoBehaviour
{
    public abstract void SetectListForMutualAimsList();

    public abstract void CreateMutualQuikSortAimsList<T>(List<T> List) where T : IDistanceToAimQuikSortable;

    public abstract List<IDistanceToAimQuikSortable> GetSortFoDistanceAimsList(Transform characterTransform);
}
