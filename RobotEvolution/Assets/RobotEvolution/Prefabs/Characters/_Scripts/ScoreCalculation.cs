using UnityEngine;
using System;

public class ScoreCalculation : MonoBehaviour
{
    [SerializeField] private CharacterRateEvolutionSO _characterRateEvolutionSO;

    private int _oldScore;
    private ICharacter _iCaracter;

    public event Action<int, int> SwapScoreThisCharacterEvent; 
    private void Awake()
    {
        TryGetComponent(out ICharacter iCharacter); _iCaracter = iCharacter;
        _iCaracter.Score = _characterRateEvolutionSO.Level_1;
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
