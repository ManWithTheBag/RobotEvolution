using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterModelStateSwitcher))]
public class CharacterModelStateCreater : MonoBehaviour
{
    private Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState> _characterModelStateDictionary;

    private void Start()
    {
        _characterModelStateDictionary = new Dictionary<CharacterModelStatsEnum, AbsCharacterBaseModetState>();

        TryGetComponent(out CharacterModelStateSwitcher characterModelStateSwitcher);

        SpawnModelsInCharacter();

        SetCharacterModetStateDictionary(characterModelStateSwitcher);
    }

    private void SpawnModelsInCharacter()
    {
        foreach (var dictionaryItem in LoadCharacterModelStateDataSO.GetDictionaryCharacterStateDataSO())
        {
            GameObject characterModel = Instantiate(dictionaryItem.Value.PrefabCharacterModel, transform);
            CreateCharacterStateDictionary(dictionaryItem, characterModel);
            SetapapingModelState(dictionaryItem, characterModel);
        }
    }

    private void CreateCharacterStateDictionary(KeyValuePair<CharacterModelStatsEnum, CharacterModelStatsDataSO> dictionaryItem, GameObject characterModel)
    {
        if (characterModel.TryGetComponent(out AbsCharacterBaseModetState characterStateClass))
            _characterModelStateDictionary.Add(dictionaryItem.Key, characterStateClass);
        else
            Debug.LogError($"LoogError: Havent CharacterState script on the model!!! Add Character script on the model:{characterModel.name}");
    }

    private void SetapapingModelState(KeyValuePair<CharacterModelStatsEnum, CharacterModelStatsDataSO> dictionaryItem, GameObject characterModel)
    {
        if (characterModel.TryGetComponent(out AbsCharacterBaseModetState characterStateClass))
            characterStateClass.SetSetupsForModelState(dictionaryItem.Value);
        else
            Debug.LogError($"LoogError: Havent CharacterState script on the model!!! Add Character script on the model:{characterModel.name}");
    }

    private void SetCharacterModetStateDictionary(CharacterModelStateSwitcher characterModelStateSwitcher)
    {
        characterModelStateSwitcher.SetCharacterModelStateDictionary(_characterModelStateDictionary);
    }
}
