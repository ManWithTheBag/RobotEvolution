using System.Collections.Generic;
using UnityEngine;

public class PoolPlayer : MonoBehaviour
{
    [Range(0, 1)][SerializeField] private int _poolCapasity;
    [SerializeField] private Player _prefabPlayer;

    private PoolMonoGC<Player> _poolPlayer;
    public List<Player> WholePlayerList { get; private set; }

    private void Start()
    {
        _poolPlayer = new PoolMonoGC<Player>(_prefabPlayer, _poolCapasity, transform);
        WholePlayerList = _poolPlayer.GetAllElementsList();
    }
}
