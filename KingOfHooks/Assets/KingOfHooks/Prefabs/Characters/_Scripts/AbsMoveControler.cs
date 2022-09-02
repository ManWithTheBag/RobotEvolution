using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjMovement), typeof(Rigidbody))]
public abstract class AbsMoveControler : MonoBehaviour, IMovable
{
    [SerializeField] protected CharacterMovementDataSO _characterMovementDataSO;
    [SerializeField] protected Transform _aimTransform;
    [SerializeField] private AnimationCurve _animaCurvePushEffect;
    //[SerializeField] private HookMoovement _hookMoovement;
    public Transform AimTransform
    {
        get { return _aimTransform; }
        set
        {
            if (value != null)
                _aimTransform = value;
            else
                _aimTransform = transform.parent;
        }
    }
    public Vector3 CurrentdirectionView { get; private set; }
    public Vector3 CurrentdirectionMove { get; private set; }
    public float CurrentSpeedMovement { get; private set; }

    private Transform _thisTransform;
    private ICharacter _iCharacter;
    private TypeMovementCharacter _currentTypeMove;
    private ObjMovement _objMovement;
    private Transform _otherBotTransform;
    private float _timeForAnimCurve;

    public virtual void OnEnable()
    {
        GlobalEventManager.OnSwapScaleCharacters.AddListener(SetSpeedForTypeMove);
    }
    public virtual void OnDisable()
    {
        GlobalEventManager.OnSwapScaleCharacters.RemoveListener(SetSpeedForTypeMove);
    }

    public virtual void Start()
    {
        TryGetComponent(out ICharacter iCharacter); _iCharacter = iCharacter;
        TryGetComponent(out ObjMovement objMovement); _objMovement = objMovement;
        _thisTransform = transform;

        SetActiveTypeMove(TypeMovementCharacter.SimpleMove);
    }

    public void SetActiveTypeMove(TypeMovementCharacter typeMove)
    {
        _timeForAnimCurve = 0;
        _currentTypeMove = typeMove;
        SetSpeedForTypeMove();
    }

    private void SetSpeedForTypeMove()
    {
        switch (_currentTypeMove)
        {
            case TypeMovementCharacter.SimpleMove:
                CurrentSpeedMovement = _characterMovementDataSO.DefoltSpeedMovement - (_iCharacter.Scale / _characterMovementDataSO.KoefDivideSpeedMotion);
                break;
            case TypeMovementCharacter.HitMoveCharacter:
                CurrentSpeedMovement = _characterMovementDataSO.DefoltSpeedMovement - (_iCharacter.Scale / _characterMovementDataSO.KoefDivideSpeedMotion);
                StartCoroutine(StopHitMove());
                break;
            case TypeMovementCharacter.PullUpMoveCharacter:
                CurrentSpeedMovement = _characterMovementDataSO.DefoltSpeedPoolUp;
                break;
            case TypeMovementCharacter.DeathCharacter:
                CurrentSpeedMovement = 0;
                break;
            default:
                CurrentSpeedMovement = _characterMovementDataSO.DefoltSpeedMovement - (_iCharacter.Scale / _characterMovementDataSO.KoefDivideSpeedMotion);
                break;
        }
    }
    IEnumerator StopHitMove()
    {
        yield return new WaitForSecondsRealtime(_characterMovementDataSO.TimeHitMove);
        SetActiveTypeMove(TypeMovementCharacter.SimpleMove);
    }

    public virtual void Update()
    {
        if (_aimTransform != null)
        {
            switch (_currentTypeMove)
            {
                case TypeMovementCharacter.SimpleMove:
                    SetDirectionForSimpleMove();
                    break;
                case TypeMovementCharacter.HitMoveCharacter:
                    SetDirectionForHitMove();
                    break;
                case TypeMovementCharacter.PullUpMoveCharacter:
                    SetDirectionForPoolUpMove();
                    break;
                case TypeMovementCharacter.DeathCharacter:
                    _objMovement.IsMovable = false;
                    break;
                default:
                    break;
            }

            _objMovement.GetDirectionTarget(this);
        }
    }

    private void SetDirectionForSimpleMove()
    {
        CurrentdirectionView = AimTransform.position - _thisTransform.position;
        CurrentdirectionMove = _thisTransform.forward;
    }
    private void SetDirectionForHitMove()
    {
        CurrentdirectionView = AimTransform.position - _thisTransform.position;
        CurrentdirectionMove = -(_otherBotTransform.position - _thisTransform.position);
        CurrentdirectionMove *= _animaCurvePushEffect.Evaluate(_timeForAnimCurve += Time.deltaTime);
    }
    private void SetDirectionForPoolUpMove()
    {
        CurrentdirectionView = AimTransform.position - _thisTransform.position;
        CurrentdirectionMove = _thisTransform.forward;
        CurrentdirectionMove *= _animaCurvePushEffect.Evaluate(_timeForAnimCurve += Time.deltaTime);
    }

    public void SetOtherBotTransform(Transform otherBotTransform)
    {
        _otherBotTransform = otherBotTransform;
    }
}
