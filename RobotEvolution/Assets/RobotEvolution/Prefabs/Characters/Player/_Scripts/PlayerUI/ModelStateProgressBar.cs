using UnityEngine;
using System.Collections;

public class ModelStateProgressBar : MonoBehaviour
{
    [SerializeField] private float _timeLearpingScoreInProgressBar;
    
    private ScoreCalculation _scoreCalculation;
    private CharacterModelStateSwitcher _characterModelStateSwitcher;
    private ModelProgressBarFields _modelProgressBarFields;
    private int _upperScoreLimit;
    private float _smoothLearpValue = 0;
    private float _currentLearpScore;
    private int _newScoreValue;

    private void OnEnable()
    {
        _scoreCalculation.SwapScoreEvent += OnRefreshModelProgressBar;
        _characterModelStateSwitcher.ChangeModelScoreLimitEvent += OnSetCurrentLimitsInModelProgressBar;
    }
    private void OnDisable()
    {
        _scoreCalculation.SwapScoreEvent -= OnRefreshModelProgressBar;
        _characterModelStateSwitcher.ChangeModelScoreLimitEvent -= OnSetCurrentLimitsInModelProgressBar;
    }

    private void Awake()
    {
        TryGetComponent(out CharacterModelStateSwitcher characterModelStateSwitcher); _characterModelStateSwitcher = characterModelStateSwitcher;
        TryGetComponent(out ScoreCalculation scoreCalculation); _scoreCalculation = scoreCalculation;

        _modelProgressBarFields = GameObject.Find("UiController").GetComponent<ModelProgressBarFields>();
        //_upperScoreLimit = _scoreCalculation.CharacterRateEvolutionSO.Level_1;
    }

    private void OnSetCurrentLimitsInModelProgressBar(int lowerScoreLimit, int upperScoreLimet)
    {
        _upperScoreLimit = upperScoreLimet;

        _modelProgressBarFields.TextLowerLImit.text = lowerScoreLimit.ToString();
        _modelProgressBarFields.TextUpperLimit.text = upperScoreLimet.ToString();
    }

    private void OnRefreshModelProgressBar(int oldScoreValue, int newScoreValue)
    {
        _newScoreValue = newScoreValue;

        StartCoroutine(LearpingScoreInProgressBar());
    }

    private IEnumerator LearpingScoreInProgressBar()
    {
        for (float i = 0; i < 1; i += Time.deltaTime / _timeLearpingScoreInProgressBar)
        {
            _currentLearpScore = Mathf.Lerp(_smoothLearpValue, _newScoreValue, i);
            _modelProgressBarFields.TextCurrentScore.text = Mathf.Round(_currentLearpScore).ToString();
            _modelProgressBarFields.ImageModelProgressBar.fillAmount = _currentLearpScore / _upperScoreLimit;
            yield return null;
        }

        _smoothLearpValue = _newScoreValue;
    }
}
