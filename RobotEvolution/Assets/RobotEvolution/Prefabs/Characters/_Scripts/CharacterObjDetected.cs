using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObjDetected : MonoBehaviour, IDetectable
{
    private ScoreCalculation _scoreCalculation;

    private void Start()
    {
        TryGetComponent(out ScoreCalculation scoreCalculation); _scoreCalculation = scoreCalculation;
    }

    public void DetectedAddScore(int score)
    {
        _scoreCalculation.AddScore(score);
    }

    public void DetectedLossScore(int score)
    {
        _scoreCalculation.LossScore(score);
    }
}
