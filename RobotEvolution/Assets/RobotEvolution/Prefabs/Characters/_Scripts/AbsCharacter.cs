using System;
using UnityEngine;

[SelectionBase]
public class AbsCharacter : MonoBehaviour, ICharacter, IRefreshible, IDistanceToAimQuikSortable, IComparable<AbsCharacter>
{
    [Min(0)][SerializeField] private int _score;
    [Min(0)] [SerializeField] private float _spawnPositionY;
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

    private RandomPosition _randomPosition;
    private Transform _thisTransform;

    private void Awake()
    {
        _thisTransform = transform;
        SortedTransform = _thisTransform;
        _randomPosition = GameObject.Find("ObjController").GetComponent<RandomPosition>();
    }

    private void OnEnable()
    {
        TotalReshreshing();
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


    public void TotalReshreshing()
    {
        transform.position = _randomPosition.GetRandomPosition(_spawnPositionY);
    }


    public void CalculateDistanceAimToCharacter(Transform characterTransform)
    {
        SortDistanceAimToCharacter = Vector3.Distance(_thisTransform.position, characterTransform.position);
    }
}
