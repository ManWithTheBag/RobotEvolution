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

    protected AimsListsContainer _aimsListsContainer;
    protected Transform _thisTransform;
    
    private List<int> _layerMaskForScan = new();
    private List<IDistanceAimsComparable> _enemyVisibleList = new();
    private List<IDistanceAimsComparable> _gearVisibleList = new();
    private List<IDistanceAimsComparable> _batteryVisibleList = new();
    private NavMeshAgent _currentNavMeshAgent;
    private NavMeshPath _navMeshPath;
    private CharacterModelStateSwitcher _characterModelStateSwitcher;
    private CharacterScanningAims _characterRayCastDetectedEnemy;

    public event Action<IAimsSelectable> ScanrdComplitEvent;

    private void Awake()
    {
        _thisTransform = transform;

        _aimsListsContainer = GetLinkAimsListsContainerClass();
        _characterRayCastDetectedEnemy = GetComponentInParent<CharacterScanningAims>();
        _currentNavMeshAgent = GetComponent<NavMeshAgent>();
        _characterModelStateSwitcher = GetComponent<CharacterModelStateSwitcher>();
    }

    private void OnEnable()
    {
        GlobalEventManager.SearchNewAimEvent.AddListener(Scanning);
        _characterModelStateSwitcher.EnterNewModelStateEvent += UpdateNavMeshAgent;
    }
    private void OnDisable()
    {
        GlobalEventManager.SearchNewAimEvent.RemoveListener(Scanning);
        _characterModelStateSwitcher.EnterNewModelStateEvent -= UpdateNavMeshAgent;
    }

    public virtual void Start()
    {
        _nearestEnemy = _thisTransform.root;
        _navMeshPath = new NavMeshPath();

        GetRandomPoint();
        Scanning();
        ActivatorScanningObj();
        StartCoroutine(TimerNewRandompoint());
    }

    private AimsListsContainer GetLinkAimsListsContainerClass()
    {
        Transform perent = _thisTransform;
        while (true)
        {
            if (!perent.TryGetComponent(out AimsListsContainer aimsListsContainer))
            {
                if (perent == _thisTransform.root)
                {
                    Debug.LogError($"LoogError: CharacterController hevent scripts {typeof(AimsListsContainer)}; Add this script to CharacterController");
                }
            }
            else
            {
                return aimsListsContainer;
            }
                perent = perent.parent;
        }
    }

    #region UpdateNavMesh
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

        foreach (RandomPoints item in _aimsListsContainer.GetRandomPointsList())
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
        _layerMaskForScan.Clear();
        _layerMaskForScan.Add(LayerMask.NameToLayer("Character"));
        _layerMaskForScan.Add(LayerMask.NameToLayer("Shield"));

        List<IDistanceAimsComparable> enemyVisibleList = LookForAims(_layerMaskForScan);

        _enemyVisibleList = enemyVisibleList;

        SetNearestEneemy(_enemyVisibleList);
    }

    public void ScanningGearVisibleList()
    {
        _layerMaskForScan.Clear();
        _layerMaskForScan.Add(LayerMask.NameToLayer("Gear"));

        List<IDistanceAimsComparable> gearVisibleList = LookForAims(_layerMaskForScan);

        _gearVisibleList = gearVisibleList;
    }

    public void ScanningBattaryVisibleList()
    {
        _layerMaskForScan.Clear();
        _layerMaskForScan.Add(LayerMask.NameToLayer("Battery"));

        List<IDistanceAimsComparable> batteryVisibleList = LookForAims(_layerMaskForScan);

        _batteryVisibleList = batteryVisibleList;
    }

    private void SetNearestEneemy(List<IDistanceAimsComparable> enemyList)
    {
        if(enemyList.Count != 0)
            _nearestEnemy = enemyList[0].SortedTransform;
    }


    #region Casculatuon Requested list
    private List<IDistanceAimsComparable> LookForAims(List<int> layerMaskForScan)
    {
        List<IDistanceAimsComparable> newVisibleAimsList = _characterRayCastDetectedEnemy.GetVisibleSoortedAimsList(layerMaskForScan);
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
