using System;
using UnityEngine;

[SelectionBase]
public abstract class AbsCharacter : MonoBehaviour, ICharacter, IRefreshible, IDistanceAimsComparable, IComparable<AbsCharacter>
{
    [Min(0)][SerializeField] private int _score;
    public int Score
    {
        get { return _score; }
        set
        {
            if (value < 0)
                _score = 0;

            else
                _score = value;
        }
    }

    [Min(0)] [SerializeField] private int _level;
    public int Level
    {
        get { return _level; }
        set
        {
            if (value < 0)
                _level = 0;

            else
                _level = value;
        }
    }

    private string _nickname;
    public string Nickname
    {
        get { return _nickname; }
        set 
        {
                _nickname = value;
        }
    }

    private int _indexPositionInLIderBoard;
    public int IndexPositionInLIderBoard
    {
        get { return _indexPositionInLIderBoard; }
        set
        {
            if (_indexPositionInLIderBoard >= 0)
                _indexPositionInLIderBoard = value;
            else
                Debug.LogError($"LoogError: Position index in LiderBoard Less 0; Index{_indexPositionInLIderBoard}; Nicknamee: {this._nickname}; Obj:{this.gameObject};") ;
        }
    }

    public float SortDistanceAimToCharacter { get; private set; }
    public Transform SortedTransform { get; private set; }

    private CharacterModelStateSwitcher _characterModelStateSwitcher;
    private RandomPosition _randomPosition;
    private Transform _thisTransform;

    public event Action CharacterRefreshedEvent;

    private void Awake()
    {
        _thisTransform = transform;
        SortedTransform = _thisTransform;
        _randomPosition = GameObject.Find("ObjController").GetComponent<RandomPosition>();
        _characterModelStateSwitcher = GetComponent<CharacterModelStateSwitcher>();
    }

    private void OnEnable()
    {
        _characterModelStateSwitcher.EnterNewModelStateEvent += SetCurrentLevel;
        _thisTransform.position = _randomPosition.GetRandomPosition();
    }
    private void OnDisable()
    {
        _characterModelStateSwitcher.EnterNewModelStateEvent -= SetCurrentLevel;
    }

    public void TotalReshreshing()
    {

        gameObject.SetActive(false);

        _thisTransform.position = _randomPosition.GetRandomPosition();

        gameObject.SetActive(true);

        CharacterRefreshedEvent?.Invoke();
    }

    public int CompareTo(AbsCharacter other)
    {
        if (other != null)
        {
            if (Score > other.Score)
                return 1;
            else if (Score < other.Score)
                return -1;
            else
                return 0;
        }
        else
        {
            Debug.LogError($"LoogError: LiderBoard can't compare elements: {this} and {other}");
            return 0;
        }
    }


    public void CalculateDistanceAimToCharacter(Transform characterTransform)
    {
        SortDistanceAimToCharacter = Vector3.Distance(_thisTransform.position, characterTransform.position);
    }

    private void SetCurrentLevel(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        _level = (int)characterModelStatsDataSO.TypeModelStateCharacter;
    }
}
