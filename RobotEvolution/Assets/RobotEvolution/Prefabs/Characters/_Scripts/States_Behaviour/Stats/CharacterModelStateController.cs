using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterModelStateController : MonoBehaviour
{
    [SerializeField] private CharacterRateEvolutionSO _characterRateEvolutionSO;
    [SerializeField] private Button _button_1, _button_2; // TODO: Delet after testing
    
    private Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState> _characterModelStateDictionary;
    [SerializeField]private ScoreCalculation _scoreCalculation;
    private AbsCharacterBaseModetState _currentCharacterModelState;
    private Transform _thisTransform;

    private void OnEnable()
    {
        _scoreCalculation.ScoreChangedEvent += OnCheckScore;
        _button_1.onClick.AddListener(SetState_1_1_WheeledBot);
        _button_2.onClick.AddListener(Set_2_1_SpiderBot);
    }

    private void OnDisable()
    {
        _scoreCalculation.ScoreChangedEvent -= OnCheckScore;
    }

    private void Start()
    {
        //TryGetComponent(out ScoreCalculation scoreCalculation); _scoreCalculation = scoreCalculation;

        _scoreCalculation.ScoreChangedEvent += OnCheckScore;
        _characterModelStateDictionary = new Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState>();

        _thisTransform = transform;

        SpawnModelsInCharacter();

        SetDefoltCharacterState();
    }

    private void SpawnModelsInCharacter()
    {
        foreach (var dictionaryItem in LoadCharacterModelStateDataSO.GetDictionaryCharacterStateDataSO())
        {
            GameObject characterModel = Instantiate(dictionaryItem.Value.PrefabCharacterModel, _thisTransform);
            CreateCharacterStateDictionary(dictionaryItem, characterModel);
            SetDefoltCharacterDataSO(dictionaryItem, characterModel);
        }
    }

    private void CreateCharacterStateDictionary(KeyValuePair<CharacterModelStatsEnum, CharacterDataSO> dictionaryItem, GameObject characterModel)
    {
        if (characterModel.TryGetComponent(out AbsCharacterBaseModetState characterStateScript))
            _characterModelStateDictionary.Add(dictionaryItem.Key, characterStateScript);
        else
            Debug.LogError($"Havent CharacterState script on the model!!! Add Character script on the model:{characterModel.name}");
    }

    private void SetDefoltCharacterDataSO(KeyValuePair<CharacterModelStatsEnum, CharacterDataSO> dictionaryItem, GameObject characterModel)
    {
        if (characterModel.TryGetComponent(out AbsCharacterBaseModetState characterStateScript))
            characterStateScript.GetCharacterDataSO(dictionaryItem.Value);
        else
            Debug.LogError($"Havent CharacterState script on the model!!! Add Character script on the model:{characterModel.name}");
    }

    private void SetDefoltCharacterState()
    {
        SetNewCharacterState(CharacterModelStatsEnum._1_1_WheeledBot);
    }

    private void SetNewCharacterState(CharacterModelStatsEnum characterStatsEnum)
    {
        if (_currentCharacterModelState != null)
            _currentCharacterModelState.Exit();

        _currentCharacterModelState = _characterModelStateDictionary[characterStatsEnum];
        _currentCharacterModelState.Enter();
    }

    public void SetState_1_1_WheeledBot()
    {
        _scoreCalculation.AddScore(10);
    }
    public void Set_2_1_SpiderBot()
    {
        _scoreCalculation.LossScore(10);
    }

    private void OnCheckScore(int oldScoreValue, int newScoreValue)
    {
        if (newScoreValue > _characterRateEvolutionSO.Level_1 && newScoreValue < _characterRateEvolutionSO.Level_2)
        {
            SetNewCharacterState(CharacterModelStatsEnum._2_1_SpiderBot);
        }
            //SetNewCharacterState(CharacterStatsEnum._2_1_SpiderBot);
        //else if (newScoreValue > _characterRateEvolutionSO.Level_2)
        //    SetNewCharacterState(CharacterStatsEnum._2_1_SpiderBot); // Chang !!!!!!!!!!!!

        //Debug.Log($"Ferst: {newScoreValue} / {_characterRateEvolutionSO.Level_1}");
       // Debug.Log($"Second: {newScoreValue} / {_characterRateEvolutionSO.Level_2}");
    }
}
