using System.Collections.Generic;
using UnityEngine;

public class PoolGear : MonoBehaviour
{
    [Min(0)][SerializeField] private int _poolCapacity;
    [SerializeField] private Gear _gearPrefab;

    private PoolMonoGC<Gear>  _poolGear;

    public List<Gear> WholeGearsList { get; private set; }

    private void Start()
    {
        _poolGear = new PoolMonoGC<Gear>(_gearPrefab, _poolCapacity, transform);
        WholeGearsList = _poolGear.GetAllElementsList();
    }
}
