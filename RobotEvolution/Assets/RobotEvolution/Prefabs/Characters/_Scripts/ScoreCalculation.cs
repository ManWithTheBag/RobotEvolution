using UnityEngine;
using System;

public class ScoreCalculation : MonoBehaviour
{
    [field: SerializeField] public CharacterRateEvolutionSO CharacterRateEvolutionSO { get; private set; }

    private int _oldScore;
    private ICharacter _iCaracter;

    public event Action<int, int> SwapScoreEvent;
    public event Action AddScoreEvent;
    public event Action LoseScoreEvent;

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

        AddScoreEvent?.Invoke();
    }

    public void LossScore(int lossScore)
    { 
        _oldScore = _iCaracter.Score;
        _iCaracter.Score -= lossScore;
        ScoreISChanged(_oldScore, _iCaracter.Score);

        LoseScoreEvent?.Invoke();
    }

    public void ScoreISChanged(int oldScoreValue, int newScoreValue)
    {
        SwapScoreEvent?.Invoke(oldScoreValue, newScoreValue);
        GlobalEventManager.SwapScoreAnyCharactersEvent.Invoke();
    }
}
