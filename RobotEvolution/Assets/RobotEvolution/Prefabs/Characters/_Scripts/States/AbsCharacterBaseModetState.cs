using System;
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

    protected AbsCharacterMovement _absCharacterMovement;
    protected CharactersAims _charactersAims;
    protected AbsCharacterModelAnimator _absCharacterModelAnimator;
    protected IShootable[] _iShootableArray;
    protected FiringRangeVisualisate _firingRangeVisualisate;
    protected Transform _thisTransform;

    private float _relativeAngle;
    private bool _isShoted = false;

    private void Awake()
    {
        _thisTransform = transform;

        TryGetComponent(out AbsCharacterModelAnimator absCharacterModelAnimator); _absCharacterModelAnimator = absCharacterModelAnimator;
        _absCharacterMovement = GetComponentInParent<AbsCharacterMovement>();
        _charactersAims = GetComponentInParent<CharactersAims>();
        _iShootableArray = GetComponentsInParent<IShootable>();

        _thisTransform.parent.TryGetComponent(out FiringRangeVisualisate firingRangeVisualisate); _firingRangeVisualisate = firingRangeVisualisate;
    }

    public abstract void Start();

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
        SetupCharacterShot();

        _absCharacterMovement.SetIsMovableCharacter(true);
    }

    public virtual void Exit()
    {
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
        if (_charactersAims.DistanceToEnemy > CharacterModelStatsDataSO.DistancePreparedToFire)
        {
            CurrentBodyView = SetCurrentBodyView(_charactersAims.NearestAimStuff);
            _isShoted = false;
        }

        else if (_charactersAims.DistanceToEnemy > CharacterModelStatsDataSO.ShotDistance && _isShoted == false)
        {
            CurrentBodyView = SetCurrentBodyView(_charactersAims.NearestAimEnemy);

            if (_charactersAims.DistanceToEnemy < CharacterModelStatsDataSO.ShotDistance - 2)
            {
                _isShoted = true;
                CurrentBodyView = SetCurrentBodyView(_charactersAims.NearestAimStuff);
            }
        }
    }



    #endregion



    #region Setup move character

    private void SetupCharacterMovable()
    {
        _absCharacterMovement.SetupingCharacterMoveent(this);
    }

    private Quaternion SetCurrentTurretView(Transform enemyTransform)
    {
        float angle = Vector3.Angle(_thisTransform.forward, enemyTransform.position - _thisTransform.position);
        if (angle < CharacterModelStatsDataSO.ViewAngle / 2f)
        {
            return LookToEnemy(enemyTransform);
        }
        else
            return LookToDefolt();
    }

    public virtual Quaternion LookToEnemy(Transform enemyTransform)
    {
        CharacterAbbleShoot(enemyTransform);

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



    #region Setup shooting character
    private void SetupCharacterShot()
    {
        foreach (IShootable item in _iShootableArray)
        {
            item.SetSetupCharacterModelForShoot(CharacterModelStatsDataSO);
        }
    }
    private void CharacterAbbleShoot(Transform enemyTransform)
    {
        foreach (IShootable item in _iShootableArray)
        {
            item.TryShootUpdate(enemyTransform, _charactersAims.DistanceToEnemy);
        }
    }
    #endregion
}
