using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsCharacterBaseModetState : MonoBehaviour
{
    [field: SerializeField] public Transform Turret { get; protected set; }

    public Quaternion CurrentBodyView { get; protected set; }
    public Quaternion CurrentTurretView { get; protected set; }
    public Vector3 CurrentCharacterMove { get; protected set; }
    public float CurrentSpeedMovement { get; protected set; }
    public CharacterModelStatsDataSO CharacterModelStatsDataSO { get; protected set; }
    public bool IsMovableCharacter { get; protected set; }

    private AbsCharacterMovement _absCharacterMovement;
    private CharactersAims _charactersAims;
    private AbsCharacterModelAnimator _absCharacterModelAnimator;
    private FiringRangeVisualisate _firingRangeVisualisate;
    private Transform _thisTransform;
    private IShootable[] _AllIShootableArray;
    private List<IShootable> _EnemaleIShootableList = new();
    private IVeaponSetupble[] _iVeaponSetapblesArray;

    private float _relativeAngle;
    private float _currentAngleToEnemy;
    private float _maxAngleViewTurrt = 0;

    private void Awake()
    {
        _thisTransform = transform;

        TryGetComponent(out AbsCharacterModelAnimator absCharacterModelAnimator); _absCharacterModelAnimator = absCharacterModelAnimator;
        _absCharacterMovement = GetComponentInParent<AbsCharacterMovement>();
        _charactersAims = GetComponentInParent<CharactersAims>();
        _iVeaponSetapblesArray = GetComponents<IVeaponSetupble>();
        _AllIShootableArray = GetComponentsInParent<IShootable>();

        _thisTransform.parent.TryGetComponent(out FiringRangeVisualisate firingRangeVisualisate); _firingRangeVisualisate = firingRangeVisualisate;
    }

    public void SetSetupsForModelState(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        CharacterModelStatsDataSO = characterModelStatsDataSO;
        CurrentSpeedMovement = characterModelStatsDataSO.SpeedMovement;
    }

    public virtual void Enter()
    {
        gameObject.SetActive(true);

        _absCharacterModelAnimator.PlayRun();

        SetupCharacterMovable();

        SetCurrentDirectionToCharacterMove();

        EnableAndSetupModelVeapons();

        GetActualIShootableArray();

        _absCharacterMovement.SetIsMovableCharacter(true);
    }

    public virtual void Exit()
    {
        DisableModelVeapons();

        gameObject.SetActive(false);
    }



    #region Setup artifical intelligence

    private void Update()
    {
        SetConstantDirectionals();

        SetVaribleDirectionals();

        SetCurrentDirectionToCharacterMove();
    }

    private void SetConstantDirectionals()
    {
        CurrentTurretView = SetCurrentTurretView(_charactersAims.NearestAimEnemy);
        CurrentCharacterMove = _thisTransform.forward;
    }

    private void SetVaribleDirectionals()
    {
        CurrentBodyView = SetCurrentBodyView(_charactersAims.NearestAimStuff);

        // ToDO: There Created artifical intelligence for earch <ModelState>!!!

        //if (_charactersAims.DistanceToEnemy > CharacterModelStatsDataSO.DistancePreparedToFire) 
        //{
        //    CurrentBodyView = SetCurrentBodyView(_charactersAims.NearestAimStuff);
        //    _isShoted = false;
        //}

        //else if (_charactersAims.DistanceToEnemy > CharacterModelStatsDataSO.ShotDistance && _isShoted == false)
        //{
        //    CurrentBodyView = SetCurrentBodyView(_charactersAims.NearestAimEnemy);

        //    if (_charactersAims.DistanceToEnemy < CharacterModelStatsDataSO.ShotDistance - 2)
        //    {
        //        _isShoted = true;
        //        CurrentBodyView = SetCurrentBodyView(_charactersAims.NearestAimStuff);
        //    }
        //}
    }
    #endregion


    #region Setup move character

    private void SetupCharacterMovable()
    {
        _absCharacterMovement.SetupingCharacterMoveent(this);
    }

    private Quaternion SetCurrentTurretView(Transform enemyTransform)
    {
        _currentAngleToEnemy = Vector3.Angle(_thisTransform.forward, enemyTransform.position - _thisTransform.position);

        if (_currentAngleToEnemy < _maxAngleViewTurrt / 2f)
        {
            return LookToEnemy(enemyTransform);
        }
        else
            return LookToDefolt();
    }

    public virtual Quaternion LookToEnemy(Transform enemyTransform)
    {
        Vector3 targetDirection = enemyTransform.position - _thisTransform.position;
        return CulculationQuaternionCharacterView(targetDirection);
    }
    public Quaternion LookToDefolt()
    {
        return CulculationQuaternionCharacterView(_thisTransform.forward);
    }

    private Quaternion SetCurrentBodyView(Transform aimTransform)
    {
        Vector3 targetDirection = aimTransform.position - _thisTransform.position;
        return CulculationQuaternionCharacterView(targetDirection);
    }

    private Quaternion CulculationQuaternionCharacterView(Vector3 targetDirection)
    {
        _relativeAngle = Mathf.Atan2(targetDirection.normalized.x, targetDirection.normalized.z) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, _relativeAngle, 0f);
    }

    private void SetCurrentDirectionToCharacterMove()
    {
        _absCharacterMovement.SetCommonDerectionViewAndMovementUpdate(this);
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
