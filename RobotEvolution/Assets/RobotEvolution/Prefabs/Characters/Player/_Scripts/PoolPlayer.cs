using System.Collections.Generic;
using UnityEngine;

public class PoolPlayer : MonoBehaviour
{
    [Range(0, 1)][SerializeField] private int _poolCapasity;
    [SerializeField] private Player _prefabPlayer;
    [SerializeField] private bool _isActiveByDefolt = false;
    [SerializeField] private bool _isAutoExpand = false;

    private PoolMonoGC<Player> _poolPlayer;
    public List<Player> WholePlayerList { get; private set; }

    private void Awake()
    {
        _poolPlayer = new PoolMonoGC<Player>(_prefabPlayer, _poolCapasity, transform, _isActiveByDefolt);
        _poolPlayer.IsAutoExpand = _isAutoExpand;
        WholePlayerList = _poolPlayer.GetAllElementsList();
    }
}
