using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class ScoreCalculation : MonoBehaviour
{
    public Button loss;
    public Button Add;

    private int _oldScore;
    private ICharacter _iCaracter;

    public event Action<int, int> OnScoreChangedEvent; 
    private void Start()
    {
        loss.onClick.AddListener(LossScore);
        Add.onClick.AddListener(AddScore);
        TryGetComponent(out ICharacter iCharacter); _iCaracter = iCharacter;
    }

    public void AddScore()
    {
        _oldScore = _iCaracter.Score;
        _iCaracter.Score += 10;
        ScoreISChanged(_oldScore, _iCaracter.Score);
    }

    public void LossScore()
    {
        _oldScore = _iCaracter.Score;
        _iCaracter.Score -= 10;
        ScoreISChanged(_oldScore, _iCaracter.Score);
    }

    public void ScoreISChanged(int oldScoreValue, int newScoreValue)
    {
        OnScoreChangedEvent?.Invoke(oldScoreValue, newScoreValue);
    }
}
