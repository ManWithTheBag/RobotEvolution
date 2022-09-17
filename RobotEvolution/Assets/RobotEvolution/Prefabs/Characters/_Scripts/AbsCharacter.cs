using System;
using UnityEngine;

[SelectionBase]
public class AbsCharacter : MonoBehaviour, ICharacter, IComparable<AbsCharacter>
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

    private RandomPosition _randomPosition;

    private void OnEnable()
    {
        _randomPosition = GameObject.Find("ObjController").GetComponent<RandomPosition>(); // TODO: Check perfomence upon complition of development

        GetRandopPosition();
    }

    private void GetRandopPosition()
    {
        transform.position = _randomPosition.GetRandomPosition(_spawnPositionY);
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
}
