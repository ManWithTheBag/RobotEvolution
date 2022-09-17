using System.Collections.Generic;
using UnityEngine;

public class PoolGear : MonoBehaviour
{
    [Min(0)][SerializeField] private int _poolCapacity;
    [SerializeField] private Gear _gearPrefab;
    [SerializeField] private bool _isActiveByDefolt = false;
    [SerializeField] private bool _isAutoExpand = false;

    private PoolMonoGC<Gear>  _poolGear;

    public List<Gear> WholeGearsList { get; private set; }

    private void Awake()
    {
        _poolGear = new PoolMonoGC<Gear>(_gearPrefab, _poolCapacity, transform, _isActiveByDefolt);
        _poolGear.IsAutoExpand = _isAutoExpand;
        WholeGearsList = _poolGear.GetAllElementsList();
    }
}
