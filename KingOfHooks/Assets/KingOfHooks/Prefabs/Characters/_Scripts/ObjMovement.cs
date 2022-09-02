using UnityEngine;

[RequireComponent(typeof(SurefaceSlider))]
public class ObjMovement : MonoBehaviour
{
    [SerializeField] private CharacterMovementDataSO _characterMovementDataSO;
    [SerializeField] Transform _checkSpherePosition;
    const float gravity = 9.81f;
    public bool IsMovable { get; set; }

    private SurefaceSlider _surefaceSlider;
    private Vector3 _directionView;
    private Vector3 _directionMove;
    private float _carentSpeedMovement;
    private Rigidbody _rb;
    private Transform _thisTransform;
    private Quaternion _targetRotation;
    private Vector3 _directionAlongSurface;
    private float _relativeAngle;
    private float _gravitySpeed;


    private void OnEnable()
    {
        IsMovable = true;
    }

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

    public void GetDirectionTarget(IMovable iMovable) // to Update
    {
        _directionView = iMovable.CurrentdirectionView.normalized;
        _directionMove = iMovable.CurrentdirectionMove.normalized;
        _carentSpeedMovement = iMovable.CurrentSpeedMovement;
    }

    private void FixedUpdate()
    {

        if (IsMovable)
        {
            RotationCharacter();
            MoveCharacter();
        }
    }

    private void RotationCharacter()
    {
        _relativeAngle = Mathf.Atan2(_directionView.x, _directionView.z) * Mathf.Rad2Deg;
        _targetRotation = Quaternion.Euler(0f, _relativeAngle, 0f);
        _thisTransform.rotation = Quaternion.Lerp(_thisTransform.rotation, _targetRotation, Time.fixedDeltaTime * _characterMovementDataSO.DefoltSpeedRotation);
    }


    private void MoveCharacter()
    {
        _directionAlongSurface = _surefaceSlider.GetDirectionAlongSurface(_directionMove);
        _rb.MovePosition(_thisTransform.position + (_directionAlongSurface * _carentSpeedMovement * Time.fixedDeltaTime));

        GravitySimulation();
    }

    private void GravitySimulation()
    {
        if (!CheckMap())
            _rb.MovePosition(_thisTransform.position + Vector3.down * gravity * Time.fixedDeltaTime);
    }

    private bool CheckMap()
    {
        return Physics.CheckSphere(_checkSpherePosition.position, _characterMovementDataSO.RadiusCheckMapSphere, _characterMovementDataSO.MapLayer);
    }
}


