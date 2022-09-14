using UnityEngine;

public abstract class AbsCharacterBaseModetState : MonoBehaviour, IMovable
{
    [field: SerializeField] public Transform Turret { get; protected set; }

    public Quaternion CurrentBodyView { get; protected set; }
    public Quaternion CurrentTurretView { get; protected set; }
    public Vector3 CurrentCharacterMove { get; protected set; }
    public float CurrentSpeedMovement { get; protected set; }
    public CharacterModelStatsDataSO CharacterModelStatsDataSO { get; protected set; }
    public bool IsMovableCharacter { get; protected set; }

    protected CharacterMovement _characterMovement;
    protected CharactersAims _charactersAims;
    protected AbsCharacterModelAnimator _absCharacterModelAnimator;
    protected IShootable[] _iShootableArray;
    protected FiringRangeVisualisate _firingRangeVisualisate;

    protected Transform _thisTransform;
    private Vector3 _targetDirection;
    private float _relativeAngle;
    //private float _distanceToEnemy;
    private bool _isShoted = false;

    private void Awake()
    {
        _thisTransform = transform;

        TryGetComponent(out AbsCharacterModelAnimator absCharacterModelAnimator); _absCharacterModelAnimator = absCharacterModelAnimator;
        _characterMovement = GetComponentInParent<CharacterMovement>();
        _charactersAims = GetComponentInParent<CharactersAims>();
        _iShootableArray = GetComponentsInParent<IShootable>();
        _firingRangeVisualisate = GetComponentInParent<FiringRangeVisualisate>();
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
        SetupFiringRangeVisualisation();

        _characterMovement.SetIsMovableCharacter(true);
    }

    public virtual void Exit()
    {
        gameObject.SetActive(false);
    }

    #region // Setup artifical intelligence

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
        //_distanceToEnemy = Vector3.Distance(_thisTransform.position, _charactersAims.NearestAimEnemy.position);

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

    #region // Setup move character

    private void SetupCharacterMovable()
    {
        _characterMovement.SetupingCharacterMoveent(this);
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
        return CulculationQuaternionCharacterView(enemyTransform);
    }

    public virtual Quaternion LookToDefolt()
    {
        return SetCharacterFrontView();
    }

    private Quaternion SetCurrentBodyView(Transform aimTransform)
    {
        return CulculationQuaternionCharacterView(aimTransform);
    }

    private Quaternion CulculationQuaternionCharacterView(Transform aimTransform)
    {
        _targetDirection = aimTransform.position - _thisTransform.position;
        _relativeAngle = Mathf.Atan2(_targetDirection.normalized.x, _targetDirection.normalized.z) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, _relativeAngle, 0f);
    }
    private Quaternion SetCharacterFrontView()
    {
        return CurrentTurretView = CurrentBodyView;
    }

    private void SetCurrentDirectionToCharacterMove()
    {
        _characterMovement.SetDerectionViewAndMovement(this);
    }

    #endregion


    #region // Setup shooting character
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



    private void SetupFiringRangeVisualisation()
    {
        _firingRangeVisualisate.SetCharacterModelDataSO(CharacterModelStatsDataSO);
    }
}
