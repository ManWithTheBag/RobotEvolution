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
                _level_2 = -999;
                Debug.LogError($"LoogError: CharacterRateEvolutionSo; Score for Level_2 < Level_1: {Level_2} < {Level_1}");
            }
        }
    }
    
    [Min(0)] [SerializeField] private int _level_3;
    public int Level_3
    {
        get { return _level_3; }
        private set
        {
            if (value > _level_2)
                _level_3 = value;
            else
            {
                _level_3 = -999;
                Debug.LogError($"LoogError: CharacterRateEvolutionSo; Score for Level_2 < Level_1: {Level_3} < {Level_2}");
            }
        }
    }
    
    [Min(0)] [SerializeField] private int _level_4;
    public int Level_4
    {
        get { return _level_4; }
        private set
        {
            if (value > _level_3)
                _level_4 = value;
            else
            {
                _level_4 = -999;
                Debug.LogError($"LoogError: Error: CharacterRateEvolutionSo; Score for Level_2 < Level_1: {Level_4} < {Level_3}");
            }
        }
    }
    
    [Min(0)] [SerializeField] private int _level_5;
    public int Level_5
    {
        get { return _level_5; }
        private set
        {
            if (value > _level_4)
                _level_5 = value;
            else
            {
                _level_5 = -999;
                Debug.LogError($"LoogError: CharacterRateEvolutionSo; Score for Level_2 < Level_1: {Level_5} < {Level_4}");
            }
        }
    }
}
