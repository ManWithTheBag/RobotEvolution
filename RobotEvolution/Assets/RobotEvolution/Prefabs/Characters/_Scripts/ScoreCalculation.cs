using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ScoreCalculation : MonoBehaviour
{
    private int _oldScore;
    private ICharacter _iCaracter;

    public event Action<int, int> ScoreChangedEvent; 
    private void Start()
    {
        TryGetComponent(out ICharacter iCharacter); _iCaracter = iCharacter;
    }

    public void AddScore(int valueAdd)
    {
        _oldScore = _iCaracter.Score;
        _iCaracter.Score += valueAdd;
        ScoreISChanged(_oldScore, _iCaracter.Score);
    }

    public void LossScore(int valueLoss)
    {
        _oldScore = _iCaracter.Score;
        _iCaracter.Score -= valueLoss;
        ScoreISChanged(_oldScore, _iCaracter.Score);
    }

    public void ScoreISChanged(int oldScoreValue, int newScoreValue)
    {
        ScoreChangedEvent?.Invoke(oldScoreValue, newScoreValue);
    }
}
