using System.Collections.Generic;
using UnityEngine;

public class PoolBattery : MonoBehaviour
{
    [Min(0)] [SerializeField] private int _poolCapacity;
    [SerializeField] private Battery _batteryPrefab;
    [SerializeField] private bool _isActiveByDefolt = false;
    [SerializeField] private bool _isAutoExpand = false;

    private PoolMonoGC<Battery> _poolBattery;

    public List<Battery> WholeBatteryList { get; private set; }

    private void Awake()
    {
        _poolBattery = new PoolMonoGC<Battery>(_batteryPrefab, _poolCapacity, transform, _isActiveByDefolt);
        _poolBattery.IsAutoExpand = _isAutoExpand;
        WholeBatteryList = _poolBattery.GetAllElementsList();
    }
}
