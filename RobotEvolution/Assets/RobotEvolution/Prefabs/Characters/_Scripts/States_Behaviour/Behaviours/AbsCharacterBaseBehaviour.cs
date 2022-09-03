using UnityEngine;

public abstract class AbsCharacterBaseBehaviour : IMovable
{
    protected ObjMovement _objMovement;
    protected Transform _aimTransform;
    protected Transform _thisCharacterTransform;
    protected CharacterMovementDataSO _characterMovementDataSO;
    protected ICharacter _iCharacter;
    protected AnimationCurve _animaCurvePushEffect;

    public Vector3 CurrentdirectionView { get; protected set; }
    public Vector3 CurrentdirectionMove { get; protected set; }
    public float CurrentSpeedMovement { get; protected set; }
    public float CurrentSpeedRotation { get; protected set; }
    public bool IsMovableCharacter { get; protected set; }

    protected AbsCharacterBaseBehaviour(AbsCharacterBehaviourController absCharacterBehaviourController)
    {
        _objMovement = absCharacterBehaviourController.ObjMovement;
        _aimTransform = absCharacterBehaviourController.AimTransform;
        _thisCharacterTransform = absCharacterBehaviourController.ThisCharacterTranshorm;
        _characterMovementDataSO = absCharacterBehaviourController.CharacterMovementDataSO;
        _iCharacter = absCharacterBehaviourController.ICharacter;
        _animaCurvePushEffect = absCharacterBehaviourController.AnimaCurvePushEffect;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void SetSpeedCharacter();
    public abstract void SetIsMovableCharacter();
    public abstract void Raning();

}
