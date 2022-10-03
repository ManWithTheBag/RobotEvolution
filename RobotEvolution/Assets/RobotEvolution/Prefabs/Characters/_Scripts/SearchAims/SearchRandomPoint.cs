using System.Collections.Generic;
using UnityEngine;

public class SearchRandomPoint : MonoBehaviour
{
    [SerializeField] private PoolRandomPoints _poolRandomPoints;
    
    public List<RandomPoints> GetRandomPointsList()
    {
        return _poolRandomPoints.WholeRandomPointsList;
    }
}
