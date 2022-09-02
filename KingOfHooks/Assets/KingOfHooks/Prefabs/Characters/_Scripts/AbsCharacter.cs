using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class AbsCharacter : MonoBehaviour, ICharacter
{
    [Min(1)][SerializeField] private float _scale;
    public float Scale
    {
        get { return _scale; }
        private set
        {
            if (value < 1)
                _scale = 1;

            else
                _scale = value;
        }
    }

    private string _nickname;
    public string Nickname
    {
        get { return _nickname; }
        set 
        {
            if (value.Length < _maxCountChurInNIcknamne)
                _nickname = value;
            else
                _nickname = "Player";
        }
    }

    private int _indexPositionInLIderBoard;
    public int IndexPositionInLIderBoard
    {
        get { return _indexPositionInLIderBoard; }
        set
        {
            if (_indexPositionInLIderBoard > 0)
                _indexPositionInLIderBoard = value;
            else
                Debug.LogError($"Position index in LiderBoard Less 0; Nicknamee: {this._nickname}; Obj:{this.gameObject}") ;
        }
    }

    private int _maxCountChurInNIcknamne = 14;
}
