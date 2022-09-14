using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreDataSO", menuName = "Scriptable Object/ScoreDataSO", order = 53)]
public class ScoreDataSO : ScriptableObject
{
    [Min(0)] [SerializeField] private int _gearScore;
    public int GearScore
    {
        get { return _gearScore; }
        private set { _gearScore = value; }
    }

    [Min(0)] [SerializeField] private int _scoreVeapoonCannon;
    public int ScoreVeapoonCannon
    {
        get { return _scoreVeapoonCannon; }
        private set { _scoreVeapoonCannon = value; }
    }

    [Min(0)] [SerializeField] private int _scoreVeapoonGunMachine;
    public int ScoreVeapoonGunMachine
    {
        get { return _scoreVeapoonGunMachine; }
        private set { _scoreVeapoonGunMachine = value; }
    }
}
