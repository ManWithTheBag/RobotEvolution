using System.Collections;
using UnityEngine;
using TMPro;

public abstract class AbsCharacterNickname : MonoBehaviour, IVisibleInvisible
{
    [SerializeField] protected NicknameDataSO _nicknameDataSO;

    protected AbsCharacter _absCharacter;
    protected TextMeshProUGUI _textNicknameScore;
    protected string _thisNickname;

    private ScoreCalculation _scoreCalculation;
    private CharacterModelStateSwitcher _characterModelStateSwitcher;
    private Transform _nicknameCharacterTransform;
    private Transform _thisTransform;
    private Transform _mainCameraTransform;
    private Transform _nicknamePrefabTransform;
    private float _currentDistanceCameraCharacter;
    private float _smoothLearpValue = 0;
    private int _currentLearpScore;
    private int _newScoreValue;


    
    public virtual void Awake()
    {
        _thisTransform = transform;
        _mainCameraTransform = Camera.main.transform;

        TryGetComponent(out AbsCharacter absCharacter); _absCharacter = absCharacter;
        TryGetComponent(out CharacterModelStateSwitcher characterModelStateSwitcher); _characterModelStateSwitcher = characterModelStateSwitcher;
        TryGetComponent(out ScoreCalculation scoreCalculation); _scoreCalculation = scoreCalculation;

        CreateNicknamePrefub();
        CreateNicknameFollowTransform();

        SetNickname();
    }

    private void OnEnable()
    {
        _characterModelStateSwitcher.EnterModelStateEvent += OnSetNicknamePosition;
        _scoreCalculation.SwapScoreThisCharacterEvent += OnRefreshCharacterScore;

        ActivateDisableNicknamePrefab(true);
    }
    private void OnDisable()
    {
        _characterModelStateSwitcher.EnterModelStateEvent -= OnSetNicknamePosition;
        _scoreCalculation.SwapScoreThisCharacterEvent -= OnRefreshCharacterScore;

        ActivateDisableNicknamePrefab(false);
    }

    private void ActivateDisableNicknamePrefab(bool isStatus)
    {
        if (_nicknamePrefabTransform != null)
            _nicknamePrefabTransform.gameObject.SetActive(false);
    }


    private void CreateNicknamePrefub()
    {
        Transform nicknameObjContater = GameObject.Find("NicknameObjContater").transform;
        _nicknamePrefabTransform = Instantiate(_nicknameDataSO.NicknamePrefab).transform;
        _nicknamePrefabTransform.SetParent(nicknameObjContater);
        _textNicknameScore = _nicknamePrefabTransform.GetComponent<TextMeshProUGUI>();
    }

    private void CreateNicknameFollowTransform()
    {
        _nicknameCharacterTransform = new GameObject("NicknameTransform").transform;
        _nicknameCharacterTransform.SetParent(_thisTransform);
    }

    private void Start()
    {
        OnRefreshCharacterScore(0, _absCharacter.Score);
        if (!gameObject.activeInHierarchy)
            ActivateDisableNicknamePrefab(false);
    }


    private void OnSetNicknamePosition(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        _nicknameCharacterTransform.localPosition = characterModelStatsDataSO.NicknamePosition;
    }

    public virtual void SetNickname()
    {
        RandomNickname randomNickname = new();
        _thisNickname = randomNickname.SetRandomNickname(_nicknameDataSO);
        _absCharacter.Nickname = _thisNickname;
    }

    public void Update()
    {
        _currentDistanceCameraCharacter = Vector3.Distance(_thisTransform.position, _mainCameraTransform.position); 

        if(_currentDistanceCameraCharacter < _nicknameDataSO.MaxDistanceVisibleNickname)
        {
            _nicknamePrefabTransform.gameObject.SetActive(true);
            _nicknamePrefabTransform.rotation = _mainCameraTransform.rotation;
            _nicknamePrefabTransform.position = _nicknameCharacterTransform.position;
        }
        else
            _nicknamePrefabTransform.gameObject.SetActive(false);
    }

    public virtual void OnRefreshCharacterScore(int oldScoreValue, int newScoreValue)
    {
        _newScoreValue = newScoreValue;
        
        StartCoroutine(LerpValue());
    }

    IEnumerator LerpValue()
    {
        for (float i = 0; i < 1; i += Time.deltaTime / _nicknameDataSO.TimeLearpingScale)
        {
            _currentLearpScore = (int)Mathf.Lerp(_smoothLearpValue, _newScoreValue, i);
            _textNicknameScore.text = _thisNickname + " = " + _currentLearpScore.ToString();
            yield return null;
        }
    }

    public void SetVisibleStatusObj(bool isStatus)
    {
        enabled = isStatus;
    }
}
