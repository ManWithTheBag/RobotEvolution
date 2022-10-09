using UnityEngine;

public abstract class AbsShield : MonoBehaviour
{
    [SerializeField] protected int _newEnergyInShield;
    [SerializeField] protected GameObject _shieldObj;
    [SerializeField] private float _timeAddOnePpintEnergy;

    protected int _maxCapasityEnergyInShield;
   
    private CharacterModelStateSwitcher _characterModelStateSwitcher;
    private ShieldDetected _shieldDetected;
    private float _timer;

    public virtual void Awake()
    {
        _characterModelStateSwitcher = GetComponentInParent<CharacterModelStateSwitcher>();
        _shieldDetected = _shieldObj.GetComponent<ShieldDetected>();
    }

    public virtual void OnEnable()
    {
        _characterModelStateSwitcher.EnterNewModelStateEvent += OnSetupShielForCurrentStateModel;
        _shieldDetected.ShieldDetectedHitEvent += OnLossEnergy;
    }
    public virtual void OnDisable()
    {
        _characterModelStateSwitcher.EnterNewModelStateEvent -= OnSetupShielForCurrentStateModel;
        _shieldDetected.ShieldDetectedHitEvent -= OnLossEnergy;
    }

    public virtual void OnSetupShielForCurrentStateModel(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        _maxCapasityEnergyInShield = characterModelStatsDataSO.MaxCapasityEnergyInShield;
        _shieldObj.transform.localPosition = new Vector3(0f, characterModelStatsDataSO.PosotionSphereShieldY, 0f);
        _shieldObj.transform.localScale = new Vector3(characterModelStatsDataSO.DiametrSphereShield, characterModelStatsDataSO.DiametrSphereShield, characterModelStatsDataSO.DiametrSphereShield);

        _newEnergyInShield = _maxCapasityEnergyInShield;
    }

    private void Start()
    {
        _shieldObj.SetActive(false);
    }

    public virtual void SetFullEnergy()
    {
        _newEnergyInShield = _maxCapasityEnergyInShield;
    }

    public virtual void OnLossEnergy(int lossEnergyValue)
    {
        _newEnergyInShield -= lossEnergyValue;

        if (_newEnergyInShield < 0)
            _newEnergyInShield = 0;

        CheckChargeBattery();
    }
    

    public virtual void CheckChargeBattery()
    {
        if (_newEnergyInShield == 0)
            _shieldObj.SetActive(false);
    }

    public virtual void OnTryActivateShield()
    {
        if(_newEnergyInShield >= _maxCapasityEnergyInShield)
            _shieldObj.SetActive(true);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(((_timer / _timeAddOnePpintEnergy) >= 1) && _newEnergyInShield < _maxCapasityEnergyInShield )
        {
            _timer = 0;

            AddOnePointEnergy();
        }
    }

    public virtual void AddOnePointEnergy()
    {
        _newEnergyInShield++;
    }
}
