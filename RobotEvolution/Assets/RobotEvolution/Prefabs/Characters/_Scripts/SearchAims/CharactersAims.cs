using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private List<IDistanceToAimQuikSortable> _pathComplitedSortAimsList = new();
    private Transform _thisTransform;
    private NavMeshAgent _currentNavMeshAgent;
    private NavMeshPath _navMeshPath;
    private CharacterModelStateSwitcher _characterModelStateSwitcher;

    private void Awake()
    {
        _thisTransform = transform;
        _searchBotsAimStuff = GetLinkSearchBotAimClass<SearchBotsAimStuff>();
        _searchBotsAimEnemy = GetLinkSearchBotAimClass<SearchBotsAimEnemy>();
        _currentNavMeshAgent = GetComponent<NavMeshAgent>();
        _characterModelStateSwitcher = GetComponent<CharacterModelStateSwitcher>();
    }

    private void OnEnable()
    {
        GlobalEventManager.SearchNewAimEvent.AddListener(OnGetOneBotAims);
        _characterModelStateSwitcher.EnterNewModelStateEvent += UpdateNavMeshAgent;
    }
    private void OnDisable()
    {
        GlobalEventManager.SearchNewAimEvent.RemoveListener(OnGetOneBotAims);
        _characterModelStateSwitcher.EnterNewModelStateEvent -= UpdateNavMeshAgent;
    }
    private void Start()
    {
        _navMeshPath = new NavMeshPath();

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

    private void UpdateNavMeshAgent(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        StartCoroutine(UpdateNavMeshAgentInEndFrame());
    }
    private IEnumerator UpdateNavMeshAgentInEndFrame()
    {
        yield return new WaitForEndOfFrame();
        _currentNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnGetOneBotAims()
    {
        GetBothTypeAimsLists();

        _nearestAimEnemy = GetPathComplitedSortAimList(1, _sortedEnemysList)[0].SortedTransform;
        _nearestAimStuff = GetPathComplitedSortAimList(1, _sortStuffsList)[0].SortedTransform;
    }

    private void GetBothTypeAimsLists()
    {
        _sortStuffsList = _searchBotsAimStuff.GetSortFoDistanceAimsList(_thisTransform);
        _sortedEnemysList = _searchBotsAimEnemy.GetSortFoDistanceAimsList(_thisTransform);
    }

    public List<IDistanceToAimQuikSortable> GetNearestAimsForIndicateArrow(int amountAimsRequest)
    {
        GetBothTypeAimsLists();

        return GetPathComplitedSortAimList(amountAimsRequest, _sortedEnemysList);
    }

    public List<IDistanceToAimQuikSortable> GetPathComplitedSortAimList(int amountAimsRequest, List<IDistanceToAimQuikSortable> checkedList)
    {

        _pathComplitedSortAimsList.Clear();

        for (int i = 0; i < checkedList.Count; i++)
        {
            _currentNavMeshAgent.CalculatePath(checkedList[i].SortedTransform.position, _navMeshPath);

            if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
                _pathComplitedSortAimsList.Add(checkedList[i]);

            if (_pathComplitedSortAimsList.Count == amountAimsRequest)
                break;
        }

        if(_pathComplitedSortAimsList.Count != amountAimsRequest)
            Debug.LogError($"LogError: Can't find complited path for aim from {checkedList}: count aims = {checkedList.Count}, count indicateArrow request = {amountAimsRequest}");

        return _pathComplitedSortAimsList;
    }

    private void Update()
    {
        DistanceToEnemy = Vector3.Distance(_thisTransform.position, NearestAimEnemy.position);
    }
}
