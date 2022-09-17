using System.Collections;
using UnityEngine;
using TMPro;

public abstract class AbsCharacterNickname : MonoBehaviour, IVisibleInvisible
{
    [SerializeField] protected NicknameDataSO _nicknameDataSO;

    [SerializeField] private CharacterModelStateSwitcher _characterModelStateSwitcher;
    [SerializeField] private ScoreCalculation _scoreCalculation;

    protected AbsCharacter _absCharacter;
    protected string _thisNickname;

    private TextMeshProUGUI _textNicknameScore;
    private Transform _nicknameCharacterTransform;
    private Transform _thisTransform;
    private Transform _mainCameraTransform;
    private Transform _nicknamePrefabTransform;
    private float _currentDistanceCameraCharacter;

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

    public virtual void Awake()
    {
        _thisTransform = transform;
        _mainCameraTransform = Camera.main.transform;

        TryGetComponent(out AbsCharacter absCharacter); _absCharacter = absCharacter;

        CreateNicknamePrefub();
        CreateNicknameFollowTransform();

        SetNickname();
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


    public void OnRefreshCharacterScore(int oldScoreValue, int newScoreValue)
    {
        StartCoroutine(LerpValue(oldScoreValue, (newScoreValue), _nicknameDataSO.TimeLearpingScale));
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

    IEnumerator LerpValue(float startValue, float endValue, float timeLearping)
    {
        float nextValue;
        for (float i = 0; i < 1; i += Time.deltaTime / timeLearping)
        {
            nextValue = Mathf.Lerp(startValue, endValue, i);
            nextValue = (int)nextValue;

            _textNicknameScore.text = _thisNickname + " = " + nextValue.ToString();
            yield return null;
        }
        _textNicknameScore.text = _thisNickname + " = " + endValue.ToString();
    }

    public void SetVisibleStatusObj(bool isStatus)
    {
        enabled = isStatus;
    }
}
