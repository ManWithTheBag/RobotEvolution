using System.Collections.Generic;
using UnityEngine;

public class LoadCharacterModelStateDataSO : MonoBehaviour
{
    public static Dictionary<CharacterModelStatsEnum, CharacterModelStatsDataSO> _characterDataSODictionary = new();
    private List<CharacterModelStatsDataSO> _characterModelStatsDataSOList = new();

    private void Awake()
    {
        LoadCharacterStatsDataSO();
    }
    private void LoadCharacterStatsDataSO()
    {
        UnityEngine.Object[] loadedModelStateDataSOArray = Resources.LoadAll("ScriptableObj/CharactersModelStatsDataSO", typeof(CharacterModelStatsDataSO));

        foreach (var item in loadedModelStateDataSOArray)
        {
            CharacterModelStatsDataSO element = (CharacterModelStatsDataSO)item;
            _characterDataSODictionary.Add(element.TypeModelStateCharacter, element);

            _characterModelStatsDataSOList.Add(element);
        }
    }

    public static Dictionary<CharacterModelStatsEnum, CharacterModelStatsDataSO> GetDictionaryCharacterStateDataSO()
    {
        return _characterDataSODictionary;
    }

    public List<CharacterModelStatsDataSO> GetloadedModelStateDataSOList()
    {
        return _characterModelStatsDataSOList;
    }
}
