using System.Collections.Generic;
using UnityEngine;

public class LoadCharacterModelStateDataSO : MonoBehaviour
{
    public static Dictionary<CharacterModelStatsEnum, CharacterModelStatsDataSO> _characterDataSODictionary = new();
    private void Awake()
    {
        LoadCharacterStatsDataSO();
    }
    private void LoadCharacterStatsDataSO()
    {
        UnityEngine.Object[] t = Resources.LoadAll("ScriptableObj/CharactersModelStatsDataSO", typeof(CharacterModelStatsDataSO));

        foreach (var item in t)
        {
            CharacterModelStatsDataSO element = (CharacterModelStatsDataSO)item;
            _characterDataSODictionary.Add(element.TypeModelStateCharacter, element);
        }
    }

    public static CharacterModelStatsDataSO GetCharacterStateDataSo(CharacterModelStatsEnum typeModelState)
    {
        return _characterDataSODictionary[typeModelState];
    }

    public static Dictionary<CharacterModelStatsEnum, CharacterModelStatsDataSO> GetDictionaryCharacterStateDataSO()
    {
        return _characterDataSODictionary;
    }
}
