using UnityEngine;

[RequireComponent(typeof(SurefaceSlider))]
public class ObjMovement : MonoBehaviour
{
    [SerializeField] Transform _checkSpherePosition;
    [Min((float)0.1)] [SerializeField] float _radiusCheckMapSphere = 0.3f;
    [SerializeField] LayerMask _mapLayer;

    const float gravity = 9.81f;

    private bool _isMovableCharacter;

    [Header("Link caching")]
    private SurefaceSlider _surefaceSlider;
    private Vector3 _directionView;
    private Vector3 _directionMove;
    private float _currentSpeedMovement;
    private float _currentSpeedRotation;
    private float _relativeAngle;
    private Rigidbody _rb;
    private Transform _thisTransform;
    private Quaternion _targetRotation;
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

    public void GetDirectionMoveAndSpeed(IMovable iMovable) // in Update
    {
        _directionView = iMovable.CurrentdirectionView.normalized;
        _directionMove = iMovable.CurrentdirectionMove.normalized;
        _currentSpeedMovement = iMovable.CurrentSpeedMove;
        _currentSpeedRotation = iMovable.CurrentSpeedRotation;
        //Debug.Log($"View = {_directionView};  Move ={_directionMove};  SpeedMove{_currentSpeedMovement};  SpeedRotation{_currentSpeedRotation}; IsMOvable ={_isMovableCharacter}");
    }

    public void GetIsMovableCharacter(IMovable iMovable)
    {
        _isMovableCharacter = iMovable.IsMovableCharacter;
    }

    private void FixedUpdate()
    {
        if (_isMovableCharacter)
        {
            RotationCharacter();
            MoveCharacter();
        }
    }

    private void RotationCharacter()
    {
        _relativeAngle = Mathf.Atan2(_directionView.x, _directionView.z) * Mathf.Rad2Deg;
        _targetRotation = Quaternion.Euler(0f, _relativeAngle, 0f);
        _thisTransform.rotation = Quaternion.Lerp(_thisTransform.rotation, _targetRotation, Time.fixedDeltaTime * _currentSpeedRotation);
    }


    private void MoveCharacter()
    {
        _directionAlongSurface = _surefaceSlider.GetDirectionAlongSurface(_directionMove);
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


