using System;
using UnityEngine;

public class BotShield : AbsShield
{
    [Min(0)][SerializeField] private float _percentRequestBattery = 10;
    [Range(1, 100)][SerializeField] private float _probablyActivationShield = 50;
    [Min(1)][SerializeField] private int _progresingRate = 5;

    private ScoreCalculation _scoreCalculation;
    private int _currentProgresingRate;

    public event Action<bool> RequestBatteryEvent;

    public override void Awake()
    {
        TryGetComponent(out ScoreCalculation scoreCalculation); _scoreCalculation = scoreCalculation;
        base.Awake();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _scoreCalculation.LoseScoreEvent += OnTryActivateShield;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        _scoreCalculation.LoseScoreEvent -= OnTryActivateShield;
    }

    public override void CheckChargeBattery()
    {
        if (_newEnergyInShield < (_maxCapasityEnergyInShield * (_percentRequestBattery / 100)))
            RequestBatteryEvent?.Invoke(true);
        else
             RequestBatteryEvent?.Invoke(false);

        base.CheckChargeBattery();
    }

    public override void OnTryActivateShield()
    {
        if (_newEnergyInShield >= _maxCapasityEnergyInShield && ProgresingRandom() > _probablyActivationShield)
        {
            _shieldObj.SetActive(true);
            _currentProgresingRate = 0;
        }
        else
            _currentProgresingRate += _progresingRate;
    }

    private float ProgresingRandom()
    {
       return UnityEngine.Random.Range(0, 100) + _progresingRate;
    }
}
