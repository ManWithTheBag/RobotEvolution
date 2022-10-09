using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(CharacterModelStateCreater))]
public class CharacterModelStateSwitcher : MonoBehaviour
{
    [SerializeField] private CharacterRateEvolutionSO _characterRateEvolutionSO;

    private ScoreCalculation _scoreCalculation;
    private AbsCharacterDeath _absCharacterDeath;
    private AbsCharacterBaseModetState _currentCharacterModelState;
    private Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState> _characterModelStateDictionary;
    private int _currentLevel;

    public event Action<CharacterModelStatsDataSO> EnterNewModelStateEvent;
    public event Action<int, int> ChangeModelScoreLimitEvent;
    public event Action FinishFinalModelRateEvent; // TODO: Invok this invent when create finish model

    private void Awake()
    {
        _scoreCalculation = GetComponent<ScoreCalculation>();
        _absCharacterDeath = GetComponent<AbsCharacterDeath>();
    }

    private void OnEnable()
    {
        _scoreCalculation.SwapScoreEvent += OnModelStateSwitch;
    }

    private void OnDisable()
    {
        _scoreCalculation.SwapScoreEvent -= OnModelStateSwitch;
    }

    public void SetCharacterModelStateDictionary(Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState> characterModelStateDictionary)
    {
        _characterModelStateDictionary = characterModelStateDictionary;
    } 

    private void SetNewCharacterState(CharacterModelStatsEnum characterStatsEnum)
    {
        if (_currentCharacterModelState != null)
            _currentCharacterModelState.Exit();

        _currentLevel = ((int)characterStatsEnum);
        _currentCharacterModelState = _characterModelStateDictionary[characterStatsEnum];

        EnterNewModelStateEvent?.Invoke(_currentCharacterModelState.CharacterModelStatsDataSO);

        _currentCharacterModelState.Enter();
    }

    private void OnModelStateSwitch(int oldScoreValue, int newScoreValue)
    {
        switch (_currentLevel)
        {
            case 0:
                CheckExitForLimitLevel_0(newScoreValue);
                break;
            case ((int)CharacterModelStatsEnum._1_1_WheeledBot):
                CheckExitForLimitLevel_1(newScoreValue);
                break;
            case ((int)CharacterModelStatsEnum._2_1_SpiderBotCrab):
                CheckExitForLimitLevel_2(newScoreValue);
                break;
            case ((int)CharacterModelStatsEnum._2_2_SpiderBotCyclop):
                CheckExitForLimitLevel_3(newScoreValue);
                break;
            case ((int)CharacterModelStatsEnum._2_3_SpiderBotElefant):
                CheckExitForLimitLevel_4(newScoreValue);
                break;
            default:
                break;
        }
    }

    private void CheckExitForLimitLevel_0(int newScoreVAlue)
    {
        if(newScoreVAlue >= _characterRateEvolutionSO.Level_1)
            SetLevel_1();
    }

    private void CheckExitForLimitLevel_1(int newScoreValue)
    {
        if (newScoreValue <= 0)
            Death();
        else if (newScoreValue >= _characterRateEvolutionSO.Level_2)
            SetLevel_2();
        else if(newScoreValue > 0 && newScoreValue < _characterRateEvolutionSO.Level_2)
            SetLevel_1();
    }

    private void CheckExitForLimitLevel_2(int newScoreValue)
    {
        if (newScoreValue < _characterRateEvolutionSO.Level_1)
            SetLevel_1();
        else if (newScoreValue >= _characterRateEvolutionSO.Level_3)
            SetLevel_3();
    }

    private void CheckExitForLimitLevel_3(int newScoreValue)
    {
        if (newScoreValue < _characterRateEvolutionSO.Level_2)
            SetLevel_2();
        else if (newScoreValue >= _characterRateEvolutionSO.Level_4)
            SetLevel_4();
    }

    private void CheckExitForLimitLevel_4(int newScoreValue)
    {
        if (newScoreValue < _characterRateEvolutionSO.Level_3)
            SetLevel_3();
        else if (newScoreValue >= _characterRateEvolutionSO.Level_5)
            SetLevel_5();
    }

    private void SetLevel_1()
    {
        SetNewCharacterState(CharacterModelStatsEnum._1_1_WheeledBot);
        ChangeModelScoreLimitEvent?.Invoke(_characterRateEvolutionSO.Level_0, _characterRateEvolutionSO.Level_2);
    }
    private void SetLevel_2()
    {
        SetNewCharacterState(CharacterModelStatsEnum._2_1_SpiderBotCrab);
        ChangeModelScoreLimitEvent?.Invoke(_characterRateEvolutionSO.Level_1, _characterRateEvolutionSO.Level_3);
    }
    private void SetLevel_3()
    {
        SetNewCharacterState(CharacterModelStatsEnum._2_3_SpiderBotElefant);
        ChangeModelScoreLimitEvent?.Invoke(_characterRateEvolutionSO.Level_2, _characterRateEvolutionSO.Level_4);
    }

    private void SetLevel_4()
    {
        SetNewCharacterState(CharacterModelStatsEnum._2_3_SpiderBotElefant);
        ChangeModelScoreLimitEvent?.Invoke(_characterRateEvolutionSO.Level_3, _characterRateEvolutionSO.Level_5);
    }
    private void SetLevel_5()
    {
        SetNewCharacterState(CharacterModelStatsEnum._3_1_Human_1);
        ChangeModelScoreLimitEvent?.Invoke(_characterRateEvolutionSO.Level_4, _characterRateEvolutionSO.Level_6);
    }


    private void Death()
    {
        _absCharacterDeath.StartDyingCharacter();
    }
}
