using System.Collections.Generic;
using UnityEngine;

public class PoolShell : MonoBehaviour
{
    [Min(0)] [SerializeField] private int _poolCapacity;
    [SerializeField] private Shell _shellPrefab;
    [SerializeField] private bool _isActiveByDefolt = false;
    [SerializeField] private bool _isAutoExpand = false;

    
    public PoolMonoGC<Shell> PoolShells { get; private set; }

    public List<Shell> WholeShellsList { get; private set; }

    private void Awake()
    {
        PoolShells = new PoolMonoGC<Shell>(_shellPrefab, _poolCapacity, transform, _isActiveByDefolt);
        PoolShells.IsAutoExpand = _isAutoExpand;
        WholeShellsList = PoolShells.GetAllElementsList();
    }
    
}
