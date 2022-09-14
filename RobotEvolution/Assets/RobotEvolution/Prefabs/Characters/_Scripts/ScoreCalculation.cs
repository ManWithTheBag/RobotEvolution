using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class ScoreCalculation : MonoBehaviour
{
    [SerializeField] private CharacterRateEvolutionSO _characterRateEvolutionSO;

    public Button loss;
    public Button Add;

    private int _oldScore;
    private ICharacter _iCaracter;

    public event Action<int, int> OnScoreChangedEvent; 
    private void Start()
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
        OnScoreChangedEvent?.Invoke(oldScoreValue, newScoreValue);
    }
}
