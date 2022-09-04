using UnityEngine;

[SelectionBase]
public class AbsCharacter : MonoBehaviour, ICharacter
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

    //private void Start()
    //{
    //    GlobalEventManager.OnSearchNewAim.Invoke();
    //}
    //private void OnEnable()
    //{
    //    GlobalEventManager.OnSearchNewAim.Invoke();
    //}
}
