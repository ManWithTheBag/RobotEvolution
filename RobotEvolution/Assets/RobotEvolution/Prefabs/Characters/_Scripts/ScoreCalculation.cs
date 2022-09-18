using UnityEngine;
using System;

public class ScoreCalculation : MonoBehaviour
{
    [field: SerializeField] public CharacterRateEvolutionSO CharacterRateEvolutionSO { get; private set; }

    private int _oldScore;
    private ICharacter _iCaracter;

    public event Action<int, int> SwapScoreThisCharacterEvent; 

    private void Start()
    {
        TryGetComponent(out ICharacter iCharacter); _iCaracter = iCharacter;
        AddScore(CharacterRateEvolutionSO.Level_1);
    }

    public void AddScore(int addScore) 
    {
        _oldScore = _iCaracter.Score;
        _iCaracter.Score += addScore;
        ScoreISChanged(_oldScore, _iCaracter.Score);
    }

    public void LossScore(int lossScore)
    { 
        _oldScore = _iCaracter.Score;
        _iCaracter.Score -= lossScore;
        ScoreISChanged(_oldScore, _iCaracter.Score);
    }

    public void ScoreISChanged(int oldScoreValue, int newScoreValue)
    {
        SwapScoreThisCharacterEvent?.Invoke(oldScoreValue, newScoreValue);
        GlobalEventManager.SwapScoreAnyCharactersEvent.Invoke();
    }
}
