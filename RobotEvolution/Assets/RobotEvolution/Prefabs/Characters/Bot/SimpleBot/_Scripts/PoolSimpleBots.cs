using System.Collections.Generic;
using UnityEngine;

public class PoolSimpleBots : MonoBehaviour
{
    [Min(0)][SerializeField] private int _poolCapasity;
    [SerializeField] private SimpleBot _prifabSimpleBot;
    [SerializeField] private bool _isActiveByDefolt = false;
    [SerializeField] private bool _isAutoExpand = false;

    private PoolMonoGC<SimpleBot> _poolSimpleBot;

    public List<SimpleBot> WholeSimpleBotList { get; private set; }
    private void Start()
    {
        _poolSimpleBot = new PoolMonoGC<SimpleBot>(_prifabSimpleBot, _poolCapasity, transform, _isActiveByDefolt);
        _poolSimpleBot.IsAutoExpand = _isAutoExpand;
        WholeSimpleBotList = _poolSimpleBot.GetAllElementsList();
    }
}
