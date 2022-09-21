using System.Collections;
using UnityEngine;

public class PlayersShield : AbsShield
{
    [SerializeField] private float _timeLearpingShieldEnergyValue;

    private ShieldProgressBarAndButtonFields _shieldProgressBarAndButtonFields;
    private float _currentEnergyInShield;
    private float _smoothLearpValue = 0;
    private bool _isLearpingShieldEnergyValue;

    public override void Awake()
    {
        _shieldProgressBarAndButtonFields = GameObject.Find("UiController").GetComponent<ShieldProgressBarAndButtonFields>();
        base.Awake();
    }

    public override void OnEnable()
    {
        _shieldProgressBarAndButtonFields.ActivateShieldB.onClick.AddListener(OnTryActivateShield);
        base.OnEnable();
    }
    public override void OnDisable()
    {
        _shieldProgressBarAndButtonFields.ActivateShieldB.onClick.RemoveListener(OnTryActivateShield);
        base.OnDisable();
    }

    public override void OnSetupShielForCurrentStateModel(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        base.OnSetupShielForCurrentStateModel(characterModelStatsDataSO);

        SetupShieldsProgressBar();
    }

    private void SetupShieldsProgressBar()
    {
        _shieldProgressBarAndButtonFields.TestCurrentEnergyValue.text = _maxCapasityEnergyInShield.ToString();
        _shieldProgressBarAndButtonFields.TextUpperLimitEnergy.text = _maxCapasityEnergyInShield.ToString();
        _shieldProgressBarAndButtonFields.TextLowerLimitEnergy.text = 0.ToString();
    }

    public override void Start()
    {
        base.Start();
        StartCoroutine(LearpingShieldEnergyValue());
    }

    public override void SetFullEnergy()
    {
        base.SetFullEnergy();
        StartCoroutine(LearpingShieldEnergyValue());
    }

    public override void OnLossEnergy(int lossEnergyValue)
    {
        base.OnLossEnergy(lossEnergyValue);

        StartCoroutine(LearpingShieldEnergyValue());
    }

    private IEnumerator LearpingShieldEnergyValue()
    {
        for (float i = 0; i < 1; i += Time.deltaTime / _timeLearpingShieldEnergyValue)
        {
            _currentEnergyInShield = Mathf.Lerp(_smoothLearpValue, _newEnergyInShield, i);
            _shieldProgressBarAndButtonFields.TestCurrentEnergyValue.text = Mathf.Round(_currentEnergyInShield).ToString();
            _shieldProgressBarAndButtonFields.ImegeFillable.fillAmount = _currentEnergyInShield / _maxCapasityEnergyInShield;

            yield return null;
        }
        _smoothLearpValue = _newEnergyInShield;
    }

    public override void AddOnePointEnergy()
    {
        base.AddOnePointEnergy();
        StartCoroutine(LearpingShieldEnergyValue());
    }
}
