using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharactersAims)), RequireComponent(typeof(FiringRangeVisualisate)), RequireComponent(typeof(CharacterRayCastDetectedEnemy))]
public abstract class AbsCharacterBaseModetState : MonoBehaviour
{
    [SerializeField] private Transform _turret;
    [Min(0)][SerializeField] private float _timerRayCastVisibleEnemy;
    public CharacterModelStatsDataSO CharacterModelStatsDataSO { get; protected set; }

    private Quaternion _currentTurretView;
    private Vector3 _currentCharacterMove;
    private AbsCharacterMovement _absCharacterMovement;
    private CharactersAims _charactersAims;
    private AbsCharacterModelAnimator _absCharacterModelAnimator;
    private FiringRangeVisualisate _firingRangeVisualisate;
    private CharacterRayCastDetectedEnemy _characterRayCastDetectedEnemy;
    private Transform _thisTransform;
    private IShootable[] _AllIShootableArray;
    private List<IShootable> _EnemaleIShootableList = new();
    private IVeaponSetupble[] _iVeaponSetapblesArray;

    private float _timerCheckVisibleNearestEnemy;
    private float _relativeAngle;
    private float _currentAngleToEnemy;
    private float _maxAngleViewTurrt = 0;

    private void Awake()
    {
        _thisTransform = transform;

        TryGetComponent(out AbsCharacterModelAnimator absCharacterModelAnimator); _absCharacterModelAnimator = absCharacterModelAnimator;
        _characterRayCastDetectedEnemy = GetComponentInParent<CharacterRayCastDetectedEnemy>();
        _absCharacterMovement = GetComponentInParent<AbsCharacterMovement>();
        _charactersAims = GetComponentInParent<CharactersAims>();
        _iVeaponSetapblesArray = GetComponents<IVeaponSetupble>();
        _AllIShootableArray = GetComponentsInParent<IShootable>();

        _thisTransform.parent.TryGetComponent(out FiringRangeVisualisate firingRangeVisualisate); _firingRangeVisualisate = firingRangeVisualisate;
    }

    public void SetSetupsForModelState(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        CharacterModelStatsDataSO = characterModelStatsDataSO;
    }

    public virtual void Enter()
    {
        gameObject.SetActive(true);

        _absCharacterModelAnimator.PlayRun();

        SetupCharacterMova();

        EnableAndSetupModelVeapons();

        GetActualIShootableArray();
    }

    public virtual void Exit()
    {
        DisableModelVeapons();

        gameObject.SetActive(false);
    }



    #region Setup artifical intelligence

    private void Update()
    {
        SetTurretDirectionals();

        SetBodyMoveTarget();

        TimerRayCastVisibleEnemy();
    }

    private void TimerRayCastVisibleEnemy()
    {
        _timerCheckVisibleNearestEnemy += Time.deltaTime / _timerCheckVisibleNearestEnemy;
        if (_timerCheckVisibleNearestEnemy > 1)
        {
            _timerCheckVisibleNearestEnemy = 0;
            //SetEnemyTransformToIndicateArrow();
        }
    }

    private void SrtVisibleNearestEnemyTransform()
    {
        _currentCharacterMove = _charactersAims.NearestAimStuff.position;
    }

    private void SetBodyMoveTarget()
    {
        _currentCharacterMove = _charactersAims.NearestAimStuff.position;

        _absCharacterMovement.SetCharacterMovePosition(_currentCharacterMove);
    }

    //private void SetVaribleDirectionals()
    //{
    //    //CurrentBodyView = SetCurrentBodyView(_charactersAims.NearestAimStuff);

    //    // ToDO: There Created artifical intelligence for earch <ModelState>!!!

    //    //if (_charactersAims.DistanceToEnemy > CharacterModelStatsDataSO.DistancePreparedToFire) 
    //    //{
    //    //    CurrentBodyView = SetCurrentBodyView(_charactersAims.NearestAimStuff);
    //    //    _isShoted = false;
    //    //}

    //    //else if (_charactersAims.DistanceToEnemy > CharacterModelStatsDataSO.ShotDistance && _isShoted == false)
    //    //{
    //    //    CurrentBodyView = SetCurrentBodyView(_charactersAims.NearestAimEnemy);

    //    //    if (_charactersAims.DistanceToEnemy < CharacterModelStatsDataSO.ShotDistance - 2)
    //    //    {
    //    //        _isShoted = true;
    //    //        CurrentBodyView = SetCurrentBodyView(_charactersAims.NearestAimStuff);
    //    //    }
    //    //}
    //}
    #endregion


    #region Setup move character
    private void SetupCharacterMova()
    {
        _absCharacterMovement.SetupMoveCharacterOneTime(CharacterModelStatsDataSO, _turret);
    }

    private void SetTurretDirectionals()
    {
        _currentTurretView = SetCurrentTurretView(_charactersAims.NearestAimEnemy);

        _absCharacterMovement.SetTurretDirectionInUpdate(_currentTurretView);
    }

    private Quaternion SetCurrentTurretView(Transform enemyTransform)
    {
        _currentAngleToEnemy = Vector3.Angle(_thisTransform.forward, enemyTransform.position - _thisTransform.position);

        if (_currentAngleToEnemy < _maxAngleViewTurrt / 2f)
            return LookToEnemy(enemyTransform);
        else
            return LookToDefolt();
    }

    public virtual Quaternion LookToEnemy(Transform enemyTransform)
    {
        Vector3 targetDirection = enemyTransform.position - _thisTransform.position;
        return CulculatQuaternionCharacterView(targetDirection);
    }
    public Quaternion LookToDefolt()
    {
        return CulculatQuaternionCharacterView(_thisTransform.forward);
    }
    private Quaternion CulculatQuaternionCharacterView(Vector3 targetDirection)
    {
        _relativeAngle = Mathf.Atan2(targetDirection.normalized.x, targetDirection.normalized.z) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, _relativeAngle, 0f);
    }

    #endregion


    #region Setup Shooting

    private void EnableAndSetupModelVeapons()
    {
        foreach (IVeaponSetupble item in _iVeaponSetapblesArray)
        {
            item.SetEvableThisVeapon();
            item.SetupVeaponForModelState();
        }
    }
    private void DisableModelVeapons()
    {
        foreach (IVeaponSetupble item in _iVeaponSetapblesArray)
        {
            item.SetDisableThisVeapon();
        }
    }


    private void GetActualIShootableArray()
    {
        _EnemaleIShootableList.Clear();

        foreach (IShootable item in _AllIShootableArray)
        {
            if (item.IsEnemleThisComponent)
                _EnemaleIShootableList.Add(item);
        }

        SetMaxAngleViewTurret();

        SetMaxDistanceFiringRangeVisualisate();
    }
    private void SetMaxAngleViewTurret()
    {
        foreach (var item in _EnemaleIShootableList)
        {
            if (_maxAngleViewTurrt < item.ViewAngleTurretAndVeapon)
                _maxAngleViewTurrt = item.ViewAngleTurretAndVeapon;
        }
    }

    private void SetMaxDistanceFiringRangeVisualisate()
    {
        if (_firingRangeVisualisate != null)
            _firingRangeVisualisate.SetMaxDistaceVisualisate(_EnemaleIShootableList.ToArray());
    }
    #endregion
}
