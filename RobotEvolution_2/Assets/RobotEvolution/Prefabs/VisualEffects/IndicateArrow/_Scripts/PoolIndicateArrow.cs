using System.Collections.Generic;
using UnityEngine;

public class PoolIndicateArrow : MonoBehaviour
{
    [field:SerializeField] public int PoolCapacity { get; private set; }

    [SerializeField] private IndicateArrow _indicateArrowPrefab;
    [SerializeField] private bool _isActiveByDefolt = false;
    [SerializeField] private bool _isAutoExpand = false;

    private PoolMonoGC<IndicateArrow> _poolIndicateArrow;

    public List<IndicateArrow> WholeIndicateArrowList { get; private set; }

    private void Awake()
    {
        _poolIndicateArrow = new PoolMonoGC<IndicateArrow>(_indicateArrowPrefab, PoolCapacity, transform, _isActiveByDefolt);
        _poolIndicateArrow.IsAutoExpand = _isAutoExpand;
        WholeIndicateArrowList = _poolIndicateArrow.GetAllElementsList();
    }
}
