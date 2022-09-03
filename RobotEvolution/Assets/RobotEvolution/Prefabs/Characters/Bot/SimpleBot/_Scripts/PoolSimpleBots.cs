using System.Collections.Generic;
using UnityEngine;

public class PoolSimpleBots : MonoBehaviour
{
    [Min(0)][SerializeField] private int _poolCapasity;
    [SerializeField] private SimpleBot _prifabSimpleBot;

    private PoolMonoGC<SimpleBot> _poolSimpleBot;

    public List<SimpleBot> WholeSimpleBotList { get; private set; }
    private void Start()
    {
        _poolSimpleBot = new PoolMonoGC<SimpleBot>(_prifabSimpleBot, _poolCapasity, transform);
        WholeSimpleBotList = _poolSimpleBot.GetAllElementsList();
    }
}
