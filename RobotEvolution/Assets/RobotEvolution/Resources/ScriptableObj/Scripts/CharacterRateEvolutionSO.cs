using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterRateEvolutionSO", menuName = "Scriptable Object/CharacterRateEvolutionSO", order = 52)]
public class CharacterRateEvolutionSO : ScriptableObject
{
    [Min(0)] [SerializeField] private int _level_1;
    public int Level_1
    {
        get { return _level_1; }
        private set { _level_1 = value; }
    }

    [Min(0)] [SerializeField] private int _level_2;
    public int Level_2
    {
        get { return _level_2; }
        private set 
        {
            if (value > _level_1)
                _level_2 = value;
            else
            {
                _level_2 = _level_1;
                Debug.LogError($"CharacterRateEvolutionSo!!! Score for Level_2 < Level_1: {Level_2} < {Level_1}");
            }
        }
    }
}
