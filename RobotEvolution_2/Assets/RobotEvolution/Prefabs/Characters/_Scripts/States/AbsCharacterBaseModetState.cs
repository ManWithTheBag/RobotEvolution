using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsCharacterBaseModetState : MonoBehaviour
{
    [SerializeField] private Transform _turret;
    public CharacterModelStatsDataSO CharacterModelStatsDataSO { get; protected set; }

    [SerializeField] private Transform _currentCharactersAim;
    private AbsCharacterMovement _absCharacterMovement;
    private CharactersAims _charactersAims;
    private CharacterModelAnimator _characterModelAnimator;
    private VeaponUpdater _veaponUpdater;
    private BotShield _botShield;

    private Transform _thisTransform;
    private bool _isEnergyRequested;
    private bool _isBattleComplited = false;
    private bool _isEnougfDistance = false;

    private void Awake()
    {
        _thisTransform = transform;

        _thisTransform.parent.TryGetComponent(out BotShield botShield); _botShield = botShield;
        _absCharacterMovement = GetComponentInParent<AbsCharacterMovement>();
        _charactersAims = GetComponentInParent<CharactersAims>();
        _veaponUpdater = GetComponent<VeaponUpdater>();

        TryGetComponent(out CharacterModelAnimator absCharacterModelAnimator); _characterModelAnimator = absCharacterModelAnimator;
    }

    private void OnEnable()
    {
        _charactersAims.ScanrdComplitEvent += OnCheckBehaviourModel;

        if (_botShield != null)
            _botShield.RequestBatteryEvent += MoveToVisibleBattery;
    }
    private void OnDisable()
    {
        _charactersAims.ScanrdComplitEvent -= OnCheckBehaviourModel;

        if (_botShield != null)
            _botShield.RequestBatteryEvent -= MoveToVisibleBattery;
    }

    public void SetSetupsForModelState(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        CharacterModelStatsDataSO = characterModelStatsDataSO;
    }

    public virtual void Enter()
    {
        gameObject.SetActive(true);

        _characterModelAnimator.PlayRun();

        _veaponUpdater.EnableModelVeapons();

        SetAimCharacter(_charactersAims.RandomPoint);

        SetupCharacterMove();
    }

    public virtual void Exit()
    {
        _veaponUpdater.DisableModelVeapons();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        SetCurrentMovePosition();
    }

    #region Setup artifical intelligence

    private void OnCheckBehaviourModel(IAimsSelectable iAimsSelectable)
    {
        if (_isEnergyRequested && iAimsSelectable.GetBatteryVisibleList().Count > 0)
        {
            _currentCharactersAim = iAimsSelectable.GetBatteryVisibleList()[0].SortedTransform;
        }

        else if (iAimsSelectable.GetEnemyVisibleList().Count > 0 && _isBattleComplited == false && _isEnougfDistance == false)
        {
            if (CheckVisibleEnemyList(iAimsSelectable.GetEnemyVisibleList()))
            {
                StartCoroutine(BattleTimeController());

                _charactersAims.NearestEnemy = iAimsSelectable.GetEnemyVisibleList()[0].SortedTransform;
                SetAimCharacter(iAimsSelectable.GetEnemyVisibleList()[0].SortedTransform);
            }
        }

        else if (iAimsSelectable.GetGearVisibleList().Count > 0)
        {
            SetAimCharacter(iAimsSelectable.GetGearVisibleList()[0].SortedTransform);
        }

        else
        {
            SetAimCharacter(_charactersAims.RandomPoint);
        }

    }


    private void MoveToVisibleBattery(bool isTurn)
    {
        _isEnergyRequested = isTurn;
    }

    #region Checked EvemyVisibleList logic
    private bool CheckVisibleEnemyList(List<IDistanceAimsComparable> newVisibleEnemyList)
    {
        return (CompareMaxCraudEnemy(newVisibleEnemyList) == true && CompareLevels(newVisibleEnemyList) == true) ? true : false;
    }

    private bool CompareMaxCraudEnemy(List<IDistanceAimsComparable> newSortedVisibleEnemyList)
    {
        return (newSortedVisibleEnemyList.Count <= CharacterModelStatsDataSO.MaxAmountCrowdEnemy) ? true : false;
    }
    private bool CompareLevels(List<IDistanceAimsComparable> newSortedVisibleEnemyList)
    {
        if (newSortedVisibleEnemyList[0].SortedTransform.TryGetComponent(out ICharacter iCharacter))
            return ((int)CharacterModelStatsDataSO.TypeModelStateCharacter >= iCharacter.Level) ? true : false;
        else
            return false;
    }

    private IEnumerator BattleTimeController()
    {
        yield return new WaitForSeconds(CharacterModelStatsDataSO.TimeBattle);
        _isBattleComplited = true;

        yield return new WaitForSeconds(CharacterModelStatsDataSO.TimeNotBattle);
        _isBattleComplited = false;
    }

    private void FixedUpdate()
    {
        if (_currentCharactersAim != null) // TODO: Make better later
        {
            if ((Vector3.Distance(_thisTransform.position, _currentCharactersAim.position) < 10))
                _isEnergyRequested = true;
            else
                _isEnergyRequested = false;
        }
    }

    #endregion


    private void SetAimCharacter(Transform aimTransform)
    {
        _currentCharactersAim = aimTransform;
    }


    #endregion

    private void SetupCharacterMove()
    {
        _absCharacterMovement.SetupMoveCharacter(CharacterModelStatsDataSO, _turret, _characterModelAnimator);
    }
    private void SetCurrentMovePosition()
    {
        if (_currentCharactersAim != null)
            _absCharacterMovement.SetCharacterMovePosition(_currentCharactersAim.position);
    }
}
