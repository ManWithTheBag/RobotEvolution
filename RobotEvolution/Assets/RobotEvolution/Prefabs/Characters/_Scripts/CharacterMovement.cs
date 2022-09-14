using UnityEngine;

[RequireComponent(typeof(SurefaceSlider))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] Transform _checkSpherePosition;
    [Min((float)0.1)] [SerializeField] float _radiusCheckMapSphere = 0.3f;
    [SerializeField] LayerMask _mapLayer;

    const float gravity = 9.81f;

    private Transform _turret;
    private Quaternion _currentBodyView;
    private Quaternion _currentTurretView;
    private Vector3 _currentCharacterMove;
    private float _currentSpeedMovement;
    private CharacterModelStatsDataSO _characterModelStatsDataSO;
    private bool _isMovableCharacter;


    private SurefaceSlider _surefaceSlider;
    private Rigidbody _rb;
    private Transform _thisTransform;
    private Vector3 _directionAlongSurface;

    private void Start()
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

    public void SetDerectionViewAndMovement(IMovable iMovable) // Update
    {
        _currentBodyView = iMovable.CurrentBodyView;
        _currentTurretView = iMovable.CurrentTurretView;
        _currentCharacterMove = iMovable.CurrentCharacterMove;
        _currentSpeedMovement = iMovable.CurrentSpeedMovement;
    }
    public void SetupingCharacterMoveent(IMovable iMovable)
    {
        _turret = iMovable.Turret;
        _characterModelStatsDataSO = iMovable.CharacterModelStatsDataSO;
    }

    public void SetIsMovableCharacter(bool turn)
    {
        _isMovableCharacter = turn;
    }

    private void FixedUpdate()
    {
        if (_isMovableCharacter && _currentBodyView != Quaternion.identity)
        {
            RotationBodyAndTurret();
            MoveCharacter();
        }
    }

    private void RotationBodyAndTurret()
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


