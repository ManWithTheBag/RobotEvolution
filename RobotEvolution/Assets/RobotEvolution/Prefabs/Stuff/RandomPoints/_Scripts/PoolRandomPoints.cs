using System.Collections.Generic;
using UnityEngine;

public class PoolRandomPoints : MonoBehaviour
{
    [Min(0)] [SerializeField] private int _poolCapacity;
    [SerializeField] private RandomPoints _randomPointsPrefab;
    [SerializeField] private bool _isActiveByDefolt = false;
    [SerializeField] private bool _isAutoExpand = false;
    [SerializeField] private RandomPosition _randomPosition;

    private PoolMonoGC<RandomPoints> _poolRandomPoints;
    
    public List<RandomPoints> WholeRandomPointsList { get; private set; }

    private void Awake()
    {
        _poolRandomPoints = new PoolMonoGC<RandomPoints>(_randomPointsPrefab, _poolCapacity, transform, _isActiveByDefolt);
        _poolRandomPoints.IsAutoExpand = _isAutoExpand;
        WholeRandomPointsList = _poolRandomPoints.GetAllElementsList();
    }

    private void Start()
    {
        foreach (var item in WholeRandomPointsList)
        {
            item.SetRandomPosition(_randomPosition.GetRandomPosition());
        }
    }
}
