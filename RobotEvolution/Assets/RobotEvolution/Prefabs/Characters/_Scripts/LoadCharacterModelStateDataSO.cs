using System.Collections.Generic;
using UnityEngine;

public class LoadCharacterModelStateDataSO : MonoBehaviour
{
    public static Dictionary<CharacterModelStatsEnum, CharacterDataSO> _characterDataSODictionary = new();
    private void Awake()
    {
        LoadCharacterStatsDataSO();
    }
    private void LoadCharacterStatsDataSO()
    {
        UnityEngine.Object[] t = Resources.LoadAll("ScriptableObj/CharactersModelStatsDataSO", typeof(CharacterDataSO));

        foreach (var item in t)
        {
            CharacterDataSO element = (CharacterDataSO)item;
            _characterDataSODictionary.Add(element.TypeModelStateCharacter, element);
        }
    }

    public static CharacterDataSO GetCharacterStateDataSo(CharacterModelStatsEnum typeModelState)
    {
        return _characterDataSODictionary[typeModelState];
    }

    public static Dictionary<CharacterModelStatsEnum, CharacterDataSO> GetDictionaryCharacterStateDataSO()
    {
        return _characterDataSODictionary;
    }
}
