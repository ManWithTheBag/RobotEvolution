using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterScanningAims))]
public class CharactersAims : MonoBehaviour, IAimsSelectable
{
    [Min(1)][SerializeField] private float _timerScanningAims = 1;
    [Min(0)] [SerializeField] private float _minDistanceRandomPoint;
    [SerializeField] private Transform _randomPoint;
    public Transform RandomPoint
    {
        get { return _randomPoint; }
        set
        {
            if (_randomPoint != null)
                _randomPoint = value;
            else
                _randomPoint = _thisTransform.root;
        }
    }

    [SerializeField] private Transform _nearestEnemy;
    public Transform NearestEnemy
    {
        get { return _nearestEnemy; }
        set 
        {
            if (_nearestEnemy != null)
                _nearestEnemy = value;
            else
                _nearestEnemy = _thisTransform.root;
        }
    }

    private List<string> _tegsForScan = new();
    private List<IDistanceAimsComparable> _enemyVisibleList = new();
    private List<IDistanceAimsComparable> _gearVisibleList = new();
    private List<IDistanceAimsComparable> _batteryVisibleList = new();
    private List<IDistanceAimsComparable> _sortedEnemysList = new();
    private SearchBotsAimEnemy _searchBotsAimEnemy;
    private SearchRandomPoint _searchRandomPoint;
    private NavMeshAgent _currentNavMeshAgent;
    private NavMeshPath _navMeshPath;
    private CharacterModelStateSwitcher _characterModelStateSwitcher;
    private CharacterScanningAims _characterRayCastDetectedEnemy;
    private Transform _thisTransform;

    public event Action<IAimsSelectable> ScanrdComplitEvent;
    //public event Action<List<IDistanceAimsComparable>> EnemyVisibleListChangedEvent;
    //public event Action<List<IDistanceAimsComparable>> GearVisibleListChangedEvent;
    //public event Action<List<IDistanceAimsComparable>> BatteryVisibleListChangedEvent;

    private void Awake()
    {
        _thisTransform = transform;

        _searchBotsAimEnemy = GetLinkSearchBotAimClass<SearchBotsAimEnemy>();
        _searchRandomPoint = GetLinkRandomPointClass<SearchRandomPoint>();

        _characterRayCastDetectedEnemy = GetComponentInParent<CharacterScanningAims>();
        _currentNavMeshAgent = GetComponent<NavMeshAgent>();
        _characterModelStateSwitcher = GetComponent<CharacterModelStateSwitcher>();
    }

    private void OnEnable()
    {
        //GlobalEventManager.SearchNewAimEvent.AddListener(OnGetBotAims);

        GlobalEventManager.SearchNewAimEvent.AddListener(Scanning);
        _characterModelStateSwitcher.EnterNewModelStateEvent += UpdateNavMeshAgent;
    }
    private void OnDisable()
    {
        //GlobalEventManager.SearchNewAimEvent.RemoveListener(OnGetBotAims);

        GlobalEventManager.SearchNewAimEvent.RemoveListener(Scanning);
        _characterModelStateSwitcher.EnterNewModelStateEvent -= UpdateNavMeshAgent;
    }

    private void Start()
    {
        _navMeshPath = new NavMeshPath();

        GetNearestEnemyListForIndicationArrow(1);
        GetRandomPoint();
        Scanning();
        ActivatorScanningObj();
        StartCoroutine(TimerNewRandompoint());
    }

    #region Setup script for strart and NewStateModet
    private T GetLinkRandomPointClass<T>() where T : SearchRandomPoint // TODO: make better later
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

    private T GetLinkSearchBotAimClass<T>() where T : AbsSearcBotshAim // TODO: make better later
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
    #endregion

    #region Get Random Point
    private void GetRandomPoint()
    {
        _randomPoint = GetRandomPointFartherMinDistance();
    }

    public Transform GetRandomPointFartherMinDistance()
    {
        List<RandomPoints> sutableRandomPoint = new();

        foreach (RandomPoints item in _searchRandomPoint.GetRandomPointsList())
        {
            float distanceToRandomPoint = Vector3.Distance(_thisTransform.position, item.GetTransformRandomPoint().position);

            if (distanceToRandomPoint > _minDistanceRandomPoint)
                sutableRandomPoint.Add(item);
        }

        return sutableRandomPoint[UnityEngine.Random.Range(0, sutableRandomPoint.Count)].GetTransformRandomPoint();
    }

    private IEnumerator TimerNewRandompoint()
    {
        yield return new WaitForSeconds(2);

        if(CheckDistanceToRandomPoint())
            GetRandomPoint();

        StartCoroutine(TimerNewRandompoint());
    }

    private bool CheckDistanceToRandomPoint()
    {
        return (Vector3.Distance(_randomPoint.position, _thisTransform.position) < _minDistanceRandomPoint / 4) ? true : false;
    }
    #endregion

    #region Activation Scanner
    private void ActivatorScanningObj()
    {
        StartCoroutine(TimerScaninngAims());
    }
    private IEnumerator TimerScaninngAims()
    {
        yield return new WaitForSeconds(RandomTimeForTimer());
        Scanning();
        ActivatorScanningObj();
    }
    private void Scanning()
    {
        ScanningEnemyVisibleList();
        ScanningGearVisibleList();
        ScanningBattaryVisibleList();

        ScanrdComplitEvent?.Invoke(this);
    }

    private float RandomTimeForTimer()
    {
        return UnityEngine.Random.Range(_timerScanningAims / 3, _timerScanningAims);
    }
    #endregion


    public void ScanningEnemyVisibleList()
    {
        _tegsForScan.Clear();
        _tegsForScan.Add("Character");
        _tegsForScan.Add("Shield");

        List<IDistanceAimsComparable> enemyVisibleList = LookForAims(_tegsForScan);

        _enemyVisibleList = enemyVisibleList;

        SetNearestEneemy(_enemyVisibleList);
    }

    public void ScanningGearVisibleList()
    {
        _tegsForScan.Clear();
        _tegsForScan.Add("Gear");

        List<IDistanceAimsComparable> gearVisibleList = LookForAims(_tegsForScan);

        _gearVisibleList = gearVisibleList;
    }

    public void ScanningBattaryVisibleList()
    {
        _tegsForScan.Clear();
        _tegsForScan.Add("Battery");

        List<IDistanceAimsComparable> batteryVisibleList = LookForAims(_tegsForScan);

        _batteryVisibleList = batteryVisibleList;
    }

    private void SetNearestEneemy(List<IDistanceAimsComparable> enemyList)
    {
        if(enemyList.Count != 0)
            _nearestEnemy = enemyList[0].SortedTransform;
    }

    public List<IDistanceAimsComparable> GetNearestEnemyListForIndicationArrow(int amountAimsRequest)
    {
        _sortedEnemysList = _searchBotsAimEnemy.GetSortFoDistanceAimsList(_thisTransform);

        SetNearestEneemy(_sortedEnemysList);

        return GetPathComplitedSortAimList(amountAimsRequest, _sortedEnemysList);
    }


    #region Casculatuon Requested list
    private List<IDistanceAimsComparable> LookForAims(List<string> tegsForScan)
    {
        List<IDistanceAimsComparable> newVisibleAimsList = _characterRayCastDetectedEnemy.GetVisibleSoortedAimsList(tegsForScan);
        return GetPathComplitedSortAimList(newVisibleAimsList.Count, newVisibleAimsList);
    }

    public List<IDistanceAimsComparable> GetPathComplitedSortAimList<T>(int amountAimsRequest, List<T> checkedList) where T : IDistanceAimsComparable
    {

        List<IDistanceAimsComparable> pathComplitedSortAimsList = new List<IDistanceAimsComparable>();

        for (int i = 0; i < checkedList.Count; i++)
        {
            _currentNavMeshAgent.CalculatePath(checkedList[i].SortedTransform.position, _navMeshPath);

            if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
                pathComplitedSortAimsList.Add(checkedList[i]);

            if (pathComplitedSortAimsList.Count == amountAimsRequest)
                break;
        }

        //if (pathComplitedSortAimsList.Count == 0)
        //    Debug.LogError($"LogError: Can't find complited path for aim from {checkedList}: " +
        //        $"amount aims = {checkedList.Count}, amount request = {amountAimsRequest}, amount found = {pathComplitedSortAimsList.Count}");

        return pathComplitedSortAimsList;
    }
    #endregion


    #region Return aims lists
    public List<IDistanceAimsComparable> GetEnemyVisibleList()
    {
        return _enemyVisibleList;
    }

    public List<IDistanceAimsComparable> GetGearVisibleList()
    {
        return _gearVisibleList;
    }

    public List<IDistanceAimsComparable> GetBatteryVisibleList()
    {
        return _batteryVisibleList;
    }
    #endregion
}
