using System.Collections.Generic;
using UnityEngine;

public class CharactersAims : MonoBehaviour
{
    [SerializeField] private Transform _nearestAimStuff;

    public Transform NearestAimStuff
    {
        get { return _nearestAimStuff; }
        set 
        {
            if (_nearestAimStuff != null)
                _nearestAimStuff = value;
            else
                _nearestAimStuff = _thisTransform.root;
    }
    }

    [SerializeField] private Transform _nearestAimEnemy;

    public Transform NearestAimEnemy
    {
        get { return _nearestAimEnemy; }
        set 
        {
            if (_nearestAimEnemy != null)
                _nearestAimEnemy = value;
            else
                _nearestAimEnemy = _thisTransform.root;
        }
    }

    public float DistanceToEnemy { get; private set; }

    private SearchBotsAimStuff _searchBotsAimStuff;
    private SearchBotsAimEnemy _searchBotsAimEnemy;
    private List<IDistanceToAimQuikSortable> _sortedEnemysList = new();
    private List<IDistanceToAimQuikSortable> _sortStuffsList = new();
    private List<IDistanceToAimQuikSortable> _nearestEnemyList = new();
    private Transform _thisTransform;

    private void Awake()
    {
        _thisTransform = transform;
        _searchBotsAimStuff = GetLinkSearchBotAimClass<SearchBotsAimStuff>();
        _searchBotsAimEnemy = GetLinkSearchBotAimClass<SearchBotsAimEnemy>();
    }

    private void OnEnable()
    {
        GlobalEventManager.SearchNewAimEvent.AddListener(OnGetOneBotAims);
    }
    private void OnDisable()
    {
        GlobalEventManager.SearchNewAimEvent.RemoveListener(OnGetOneBotAims);
    }
    private void Start()
    {
        OnGetOneBotAims();
    }
    private T GetLinkSearchBotAimClass<T>() where T : AbsSearcBotshAim
    {
        Transform perent = _thisTransform.parent;
        while (true)
        {
            if (!perent.TryGetComponent(out T searchBotsAimClass))
            {
                perent = perent.parent;
                if (perent == _thisTransform.root)
                {
                    Debug.LogError($"LoogError: CharacterController hevent scripts {typeof(SearchBotsAimEnemy)}; Add this script to CharacterController");
                }
            }
            else
            {
                return searchBotsAimClass;
            }
        }
    }


    private void OnGetOneBotAims()
    {
        GetBothTypeAimsLists();

        _nearestAimStuff = _sortStuffsList[0].SortedTransform;
        _nearestAimEnemy = _sortedEnemysList[0].SortedTransform;
    }

    public List<IDistanceToAimQuikSortable> GetNearestlyEnemyList(int coutArrowRequest)
    {
        GetBothTypeAimsLists();

        _nearestEnemyList.Clear();

        for (int i = 0; i < coutArrowRequest; i++)
        {
            if (_sortedEnemysList.Count > i)
                _nearestEnemyList.Add(_sortedEnemysList[i]);
            else
                Debug.LogError($"LogError: Request cout indicate arrow more then enemy in game: count enemy = {_sortedEnemysList.Count}, count indicateArrow request = {coutArrowRequest}");
        }

        return _nearestEnemyList;
    }

    private void GetBothTypeAimsLists()
    {
        _sortStuffsList = _searchBotsAimStuff.GetSortFoDistanceAimsList(_thisTransform);
        _sortedEnemysList = _searchBotsAimEnemy.GetSortFoDistanceAimsList(_thisTransform);
    }

    private void Update()
    {
        DistanceToEnemy = Vector3.Distance(_thisTransform.position, NearestAimEnemy.position);
    }
}
