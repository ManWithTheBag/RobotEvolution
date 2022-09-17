using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterModelStateCreater))]
public class CharacterModelStateSwitcher : MonoBehaviour
{
    [SerializeField] private CharacterRateEvolutionSO _characterRateEvolutionSO;
    [SerializeField] private ScoreCalculation _scoreCalculation;

    private AbsCharacterBaseModetState _currentCharacterModelState;
    private Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState> _characterModelStateDictionary;
    private int _currentLevel;

    public event System.Action<CharacterModelStatsDataSO> EnterModelStateEvent;

    private void OnEnable()
    {
        _scoreCalculation.SwapScoreThisCharacterEvent += OnModelStateSwitch;
    }

    private void OnDisable()
    {
        _scoreCalculation.SwapScoreThisCharacterEvent -= OnModelStateSwitch;
    }

    private void Start()
    {
        _currentLevel = ((int)CharacterModelStatsEnum._1_1_WheeledBot);
    }

    public void SetCharacterModelStateDictionary(Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState> characterModelStateDictionary)
    {
        _characterModelStateDictionary = characterModelStateDictionary;
    } 

    public void SetDefoltCharacterModelState()
    {
        SetNewCharacterState(CharacterModelStatsEnum._1_1_WheeledBot);
    }

    private void SetNewCharacterState(CharacterModelStatsEnum characterStatsEnum)
    {
        if (_currentCharacterModelState != null)
            _currentCharacterModelState.Exit();

        _currentLevel = ((int)characterStatsEnum);
        _currentCharacterModelState = _characterModelStateDictionary[characterStatsEnum];

        EnterModelStateEvent?.Invoke(_currentCharacterModelState.CharacterModelStatsDataSO);

        _currentCharacterModelState.Enter();
    }

    private void OnModelStateSwitch(int oldScoreValue, int newScoreValue)
    {
        switch (_currentLevel)
        {
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

    private void CheckExitForLimitLevel_1(int newScoreValue)
    {
        if (newScoreValue <= 0)
            Death();
        else if (newScoreValue > _characterRateEvolutionSO.Level_2)
            SetNewCharacterState(CharacterModelStatsEnum._2_1_SpiderBotCrab);
    }

    private void CheckExitForLimitLevel_2(int newScoreValue)
    {
        if (newScoreValue < _characterRateEvolutionSO.Level_1)
            SetNewCharacterState(CharacterModelStatsEnum._1_1_WheeledBot);
        else if (newScoreValue > _characterRateEvolutionSO.Level_3)
            SetNewCharacterState(CharacterModelStatsEnum._2_2_SpiderBotCyclop);
    }

    private void CheckExitForLimitLevel_3(int newScoreValue)
    {
        if (newScoreValue < _characterRateEvolutionSO.Level_2)
            SetNewCharacterState(CharacterModelStatsEnum._2_1_SpiderBotCrab);
        else if (newScoreValue > _characterRateEvolutionSO.Level_4)
            SetNewCharacterState(CharacterModelStatsEnum._2_3_SpiderBotElefant);
    }

    private void CheckExitForLimitLevel_4(int newScoreValue)
    {
        if (newScoreValue < _characterRateEvolutionSO.Level_3)
            SetNewCharacterState(CharacterModelStatsEnum._2_3_SpiderBotElefant);
        else if (newScoreValue > _characterRateEvolutionSO.Level_5)
            SetNewCharacterState(CharacterModelStatsEnum._3_1_Human_1);
    }



    private void Death()
    {

    }
}
