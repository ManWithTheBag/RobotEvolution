using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterModelStateCreater))]
public class CharacterModelStateSwitcher : MonoBehaviour
{
    [SerializeField] private CharacterRateEvolutionSO _characterRateEvolutionSO;
    [SerializeField] private ScoreCalculation _scoreCalculation;

    private AbsCharacterBaseModetState _currentCharacterModelState;
    private Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState> _characterModelStateDictionary;
    private void OnEnable()
    {
        _scoreCalculation.OnScoreChangedEvent += OnModelStateSwitch;
    }

    private void OnDisable()
    {
        _scoreCalculation.OnScoreChangedEvent -= OnModelStateSwitch;
    }

    public void SetCharacterModelStateDictionary(Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState> characterModelStateDictionary)
    {
        _characterModelStateDictionary = characterModelStateDictionary;
    } 

    public void SetDefoltCharacterModelState()
    {
        SetNewCharacterState(CharacterModelStatsEnum._1_1_WheeledBot);
    }

    private void OnModelStateSwitch(int oldScoreValue, int newScoreValue)
    {
        if (newScoreValue < _characterRateEvolutionSO.Level_1)
        {
            SetNewCharacterState(CharacterModelStatsEnum._1_1_WheeledBot);
        }
        if (newScoreValue > _characterRateEvolutionSO.Level_1 && newScoreValue < _characterRateEvolutionSO.Level_2)
        {
            SetNewCharacterState(CharacterModelStatsEnum._2_1_SpiderBot);
        }
    }

    private void SetNewCharacterState(CharacterModelStatsEnum characterStatsEnum)
    {
        if (_currentCharacterModelState != null)
            _currentCharacterModelState.Exit();

        _currentCharacterModelState = _characterModelStateDictionary[characterStatsEnum];
        _currentCharacterModelState.Enter();
    }
}
