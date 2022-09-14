using UnityEngine;

public class SearchBotsAimEnemy : AbsSearcBotshAim
{
    [SerializeField] private PoolPlayer _poolPlayer;
    [SerializeField] private PoolSimpleBots _poolSimpleBots;

    public override void SelectSearcingAimLists()
    {
        SearchNearestAimInList<Player>(ActiveCharactersInList.GetActiveCharactersInList(_poolPlayer.WholePlayerList));
        SearchNearestAimInList<SimpleBot>(ActiveCharactersInList.GetActiveCharactersInList(_poolSimpleBots.WholeSimpleBotList));
    }
}
