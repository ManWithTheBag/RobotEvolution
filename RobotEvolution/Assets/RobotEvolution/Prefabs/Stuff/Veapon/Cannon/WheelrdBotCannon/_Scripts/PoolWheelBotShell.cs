using System.Collections.Generic;
using UnityEngine;

public class PoolWheelBotShell : MonoBehaviour, IPoolUsable
{
    [Min(0)] [SerializeField] private int _poolCapacity;
    [SerializeField] private WhillBotShellCannon _shellPrefab;
    [SerializeField] private bool _isActiveByDefolt = false;
    [SerializeField] private bool _isAutoExpand = false;

    
    public PoolMonoGC<WhillBotShellCannon> PoolShells { get; private set; }

    public List<WhillBotShellCannon> WholeShellsList { get; private set; }


    private void Awake()
    {
        PoolShells = new PoolMonoGC<WhillBotShellCannon>(_shellPrefab, _poolCapacity, transform, _isActiveByDefolt);
        PoolShells.IsAutoExpand = _isAutoExpand;
        WholeShellsList = PoolShells.GetAllElementsList();
    }

    public Transform GetFreeElement()
    {
        return PoolShells.GetFreeElement().transform;
    }
    
}
