using UnityEngine;
using System.Collections;

public class ModelStateProgressBar : MonoBehaviour
{
    [SerializeField]private ScoreCalculation _scoreCalculation;
    [SerializeField]private CharacterModelStateSwitcher _characterModelStateSwitcher;
    [SerializeField] private float _timeLearpingScoreInProgressBar;
    
    private ModelProgressBarFields _modelProgressBarFields;
    private bool _isScoreLeearped = true;
    private int _upperScoreLimit;
    private float _smoothLearpValue = 0;
    private float _currentLearpScore;

    private void OnEnable()
    {
        _scoreCalculation.SwapScoreThisCharacterEvent += RefreshModelProgressBar;
        _characterModelStateSwitcher.ChangeModelScoreLimetEvent += SetCurrentLimitsInModelProgressBar;
    }
    private void OnDisable()
    {
        _scoreCalculation.SwapScoreThisCharacterEvent -= RefreshModelProgressBar;
        _characterModelStateSwitcher.ChangeModelScoreLimetEvent -= SetCurrentLimitsInModelProgressBar;
    }

    private void Awake()
    {
        _modelProgressBarFields = GameObject.Find("UiController").GetComponent<ModelProgressBarFields>();
        _upperScoreLimit = _scoreCalculation.CharacterRateEvolutionSO.Level_1;
    }

    private void SetCurrentLimitsInModelProgressBar(int lowerScoreLimit, int upperScoreLimet)
    {
        _upperScoreLimit = upperScoreLimet;

        _modelProgressBarFields.TextLowerLImit.text = lowerScoreLimit.ToString();
        _modelProgressBarFields.TextUpperLimit.text = upperScoreLimet.ToString();
    }


    private void RefreshModelProgressBar(int oldScoreValue, int newScoreValue)
    {
        if (_isScoreLeearped)
            StartCoroutine(LearpingScoreInProgressBar(oldScoreValue, newScoreValue));
    }

    private IEnumerator LearpingScoreInProgressBar(int oldScoreValue, int newScoreValue)
    {
        _isScoreLeearped = false;

        for (float i = 0; i < 1; i += Time.deltaTime / _timeLearpingScoreInProgressBar)
        {
            _currentLearpScore = Mathf.Lerp(_smoothLearpValue, newScoreValue, i);
            _modelProgressBarFields.TextCurrentScore.text = Mathf.Round(_currentLearpScore).ToString();
            _modelProgressBarFields.ImageModelProgressBar.fillAmount = _currentLearpScore / _upperScoreLimit;
            yield return null;
        }
        _modelProgressBarFields.TextCurrentScore.text = newScoreValue.ToString();
        _modelProgressBarFields.ImageModelProgressBar.fillAmount = (float)newScoreValue / _upperScoreLimit;
        _smoothLearpValue = newScoreValue;

        _isScoreLeearped = true;
    }
}
