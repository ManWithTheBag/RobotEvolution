using UnityEngine;

[RequireComponent(typeof(SurefaceSlider))]
public abstract class AbsCharacterMovement : MonoBehaviour
{
    [SerializeField] Transform _checkSpherePosition;
    [Min((float)0.1)] [SerializeField] float _radiusCheckMapSphere = 0.3f;
    [SerializeField] LayerMask _mapLayer;

    const float gravity = 9.81f;

    protected Quaternion _currentBodyView;
    protected Quaternion _currentTurretView;
    protected Vector3 _currentCharacterMove;
    protected Transform _thisTransform;
    protected bool _isMovableCharacter;

    private Transform _turret;
    private CharacterModelStatsDataSO _characterModelStatsDataSO;
    private SurefaceSlider _surefaceSlider;
    private Rigidbody _rb;
    private Vector3 _directionAlongSurface;
    private float _currentSpeedMovement;
    
    public virtual void Awake()
    {
        TryGetComponent(out Rigidbody rb); _rb = rb;
        TryGetComponent(out SurefaceSlider surefaceSlider); _surefaceSlider = surefaceSlider;

        _thisTransform = transform;

        SetupRb();
    }

    private void SetupRb()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _rb.mass = 60;  // TODO: make varible for this (_rb.mass = 60)
    }

    public void SetCommonDerectionViewAndMovementUpdate(AbsCharacterBaseModetState absCharacterBaseModetState)
    {
        _turret = absCharacterBaseModetState.Turret;
        _currentTurretView = absCharacterBaseModetState.CurrentTurretView;
        _currentSpeedMovement = absCharacterBaseModetState.CurrentSpeedMovement;

        SetIndividualViewAndMove(absCharacterBaseModetState);
    }

    public virtual void SetIndividualViewAndMove(AbsCharacterBaseModetState absCharacterBaseModetState)
    {
        _currentBodyView = absCharacterBaseModetState.CurrentBodyView;
        _currentCharacterMove = absCharacterBaseModetState.CurrentCharacterMove;
    }


    public void SetupingCharacterMoveent(AbsCharacterBaseModetState absCharacterBaseModetState)
    {
        _characterModelStatsDataSO = absCharacterBaseModetState.CharacterModelStatsDataSO;
    }

    public void SetIsMovableCharacter(bool turn)
    {
        _isMovableCharacter = turn;
    }

    private void FixedUpdate()
    {
        if (_isMovableCharacter)
        {
            RotationBody();
            //MoveCharacter();
        }
    }

    private void RotationBody()
    {
        _thisTransform.rotation = Quaternion.Lerp(_thisTransform.rotation, _currentBodyView, Time.fixedDeltaTime * _characterModelStatsDataSO.SpeedBodyRotation);
        _turret.rotation = Quaternion.Lerp(_turret.rotation, _currentTurretView, Time.fixedDeltaTime * _characterModelStatsDataSO.SpeedTurretRotation);
    }

    private void MoveCharacter()
    {
        _directionAlongSurface = _surefaceSlider.GetDirectionAlongSurface(_currentCharacterMove);
        _rb.MovePosition(_thisTransform.position + (_directionAlongSurface * _currentSpeedMovement * Time.fixedDeltaTime));

        GravitySimulation();
    }

    private void GravitySimulation()
    {
        if (!CheckMap())
            _rb.MovePosition(_thisTransform.position + Vector3.down * gravity * Time.fixedDeltaTime);
    }

    private bool CheckMap()
    {
        return Physics.CheckSphere(_checkSpherePosition.position, _radiusCheckMapSphere, _mapLayer);
    }
}


