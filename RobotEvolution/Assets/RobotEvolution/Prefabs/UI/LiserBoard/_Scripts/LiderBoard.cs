using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiderBoard : MonoBehaviour
{
    [SerializeField] private PoolSimpleBots _poolSimpleBots;
    [SerializeField] private PoolPlayer _poolPlayer;
    [SerializeField] private GameObject _liderBoardTemplat;
    [SerializeField] private Transform _containerTextTemplats;
    [Min(0)][SerializeField] private float _spacingY;
    [Min(0)][SerializeField] private float _widthTemplat;

    private List<float> _positionYList = new();
    private List<RectTransform> _textTemplatsRectTransformList = new();
    private List<AbsCharacter> _allAbsCharacterInGame = new();
    private bool _isLearped = true;
    private int _countCharactersInGame;

    private void OnEnable()
    {
        GlobalEventManager.SwapScoreAnyCharactersEvent.AddListener(RefreshLiderBoard);
    }

    private void OnDisable()
    {
        GlobalEventManager.SwapScoreAnyCharactersEvent.RemoveListener(RefreshLiderBoard);
    }

    private void Start()
    {
        _countCharactersInGame = _poolSimpleBots.WholeSimpleBotList.Count + _poolPlayer.WholePlayerList.Count;

        CreateMutualList<SimpleBot>(_poolSimpleBots.WholeSimpleBotList);
        CreateMutualList<Player>(_poolPlayer.WholePlayerList);

        SetTemplaitYposition();
        CreateTextTemplate();
        SetIndividuaIndexPositionInLiderBoard();
        RefreshLiderBoard();
    }

    //private List<AbsCharacter> _allAbsCharacterInGame = new();
    //private List<List<AbsCharacter>> _testListList =
    private void CreateMutualList<T>(List<T> list) where T : AbsCharacter
    {
        //_testListList.Add(list);
        _allAbsCharacterInGame.AddRange(list);
    }

    private void SetTemplaitYposition()
    {
        for (int i = 0; i < _countCharactersInGame; i++)
        {
            _positionYList.Add(_spacingY * (-i));
        }
    }

    private void CreateTextTemplate()
    {
        for (int i = 0; i < _countCharactersInGame; i++)
        {
            GameObject temporaryObj = Instantiate(_liderBoardTemplat, _containerTextTemplats);
            temporaryObj.TryGetComponent(out RectTransform rectTransform);
            rectTransform.offsetMin = new Vector2(0, _positionYList[i]);
            rectTransform.offsetMax = new Vector2(_widthTemplat, _positionYList[i]);
            rectTransform.sizeDelta = new Vector2(_widthTemplat, 0);
            _textTemplatsRectTransformList.Add(rectTransform);
        }
    }

    private void SetIndividuaIndexPositionInLiderBoard()
    {
        for (int i = 0; i < _countCharactersInGame; i++)
        {
            _allAbsCharacterInGame[i].IndexPositionInLIderBoard = i;
        }
    }

    public void RefreshLiderBoard()
    {
        if (_isLearped && _allAbsCharacterInGame.Count != 0)
        {
            _isLearped = false;

            _allAbsCharacterInGame.Sort();
            _allAbsCharacterInGame.Reverse();

            for (int i = 0; i < _countCharactersInGame; i++)
            {
                _textTemplatsRectTransformList[_allAbsCharacterInGame[i].IndexPositionInLIderBoard].TryGetComponent(out LiderBoardTextTemplats liderBoardTextTemplats);
                liderBoardTextTemplats.TextNickname.text = _allAbsCharacterInGame[i].Nickname;
                liderBoardTextTemplats.TextScore.text = _allAbsCharacterInGame[i].Score.ToString();
                StartCoroutine(LearpingPositionInLiderBoard(_textTemplatsRectTransformList[_allAbsCharacterInGame[i].IndexPositionInLIderBoard], i));
            }
        }
    }

    private IEnumerator LearpingPositionInLiderBoard(RectTransform rectTransform, int newPosition)
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            rectTransform.offsetMin = Vector2.Lerp(rectTransform.offsetMin, new Vector2(0, _positionYList[newPosition]), i);
            rectTransform.offsetMax = Vector2.Lerp(rectTransform.offsetMax, new Vector2(150, _positionYList[newPosition]), i);
            yield return null;
        }

        rectTransform.offsetMin = new Vector2(0, _positionYList[newPosition]);
        rectTransform.offsetMax = new Vector2(150, _positionYList[newPosition]);

        _isLearped = true;
    }
}
