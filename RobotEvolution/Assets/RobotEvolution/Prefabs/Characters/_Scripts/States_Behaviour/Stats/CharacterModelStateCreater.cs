using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterModelStateSwitcher))]
public class CharacterModelStateCreater : MonoBehaviour
{
    private CharactersAims _charactersAims;
    private AbsCharacterBehaviourController _absCharacterBehaviourController;
    private Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState> _characterModelStateDictionary;
    private Transform _thisTransform;

    private void Start()
    {
        _characterModelStateDictionary = new Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState>();

        TryGetComponent(out CharactersAims charactersAims); _charactersAims = charactersAims;
        TryGetComponent(out AbsCharacterBehaviourController absCharacterBehaviourController); _absCharacterBehaviourController = absCharacterBehaviourController;

        TryGetComponent(out CharacterModelStateSwitcher characterModelStateSwitcher);

        _thisTransform = transform;

        SpawnModelsInCharacter();

        SetCCharacterModetStateDictionary(characterModelStateSwitcher);

        SetDefoltCharacterModelState(characterModelStateSwitcher);

    }

    private void SpawnModelsInCharacter()
    {
        foreach (var dictionaryItem in LoadCharacterModelStateDataSO.GetDictionaryCharacterStateDataSO())
        {
            GameObject characterModel = Instantiate(dictionaryItem.Value.PrefabCharacterModel, _thisTransform);
            CreateCharacterStateDictionary(dictionaryItem, characterModel);
            SetapapingModelState(dictionaryItem, characterModel);
        }
    }

    private void CreateCharacterStateDictionary(KeyValuePair<CharacterModelStatsEnum, CharacterModelStatsDataSO> dictionaryItem, GameObject characterModel)
    {
        if (characterModel.TryGetComponent(out AbsCharacterBaseModetState characterStateClass))
            _characterModelStateDictionary.Add(dictionaryItem.Key, characterStateClass);
        else
            Debug.LogError($"Havent CharacterState script on the model!!! Add Character script on the model:{characterModel.name}");
    }

    private void SetapapingModelState(KeyValuePair<CharacterModelStatsEnum, CharacterModelStatsDataSO> dictionaryItem, GameObject characterModel)
    {
        if (characterModel.TryGetComponent(out AbsCharacterBaseModetState characterStateClass))
        {
            characterStateClass.SetSetapsForModelState(dictionaryItem.Value, _charactersAims, _absCharacterBehaviourController);
        }
        else
            Debug.LogError($"Havent CharacterState script on the model!!! Add Character script on the model:{characterModel.name}");
    }

    private void SetCCharacterModetStateDictionary(CharacterModelStateSwitcher characterModelStateSwitcher)
    {
        characterModelStateSwitcher.SetCharacterModelStateDictionary(_characterModelStateDictionary);
    }

    private void SetDefoltCharacterModelState(CharacterModelStateSwitcher characterModelStateSwitcher)
    {
        characterModelStateSwitcher.SetDefoltCharacterModelState();
    }
}
