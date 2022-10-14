using System.Collections.Generic;
using UnityEngine;

public class PoolPanzerCannon : MonoBehaviour, IPoolUsable
{
    [Min(0)] [SerializeField] private int _poolCapacity;
    [SerializeField] private PanzerShellCannon _panzerShellPrefab;
    [SerializeField] private bool _isActiveByDefolt = false;
    [SerializeField] private bool _isAutoExpand = false;


    public PoolMonoGC<PanzerShellCannon> PoolPanzerShells { get; private set; }

    public List<PanzerShellCannon> WholePanzerShellsList { get; private set; }

    private void Awake()
    {
        PoolPanzerShells = new PoolMonoGC<PanzerShellCannon>(_panzerShellPrefab, _poolCapacity, transform, _isActiveByDefolt);
        PoolPanzerShells.IsAutoExpand = _isAutoExpand;
        WholePanzerShellsList = PoolPanzerShells.GetAllElementsList();
    }

    public Transform GetFreeElement()
    {
        return PoolPanzerShells.GetFreeElement().transform;
    }

}
