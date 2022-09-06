using UnityEngine;

public abstract class AbsCharacterBaseBehaviour : IMovable
{
    protected ObjMovement _objMovement;
    protected AnimationCurve _animaCurvePushEffect;

    public Vector3 CurrentdirectionView { get; protected set; }
    public Vector3 CurrentdirectionMove { get; protected set; }
    public float CurrentSpeedMove { get; protected set; }
    public float CurrentSpeedRotation { get; protected set; }
    public bool IsMovableCharacter { get; protected set; }

    protected AbsCharacterBaseBehaviour(AbsCharacterBehaviourController absCharacterBehaviourController)
    {
        _objMovement = absCharacterBehaviourController.ObjMovement;
        _animaCurvePushEffect = absCharacterBehaviourController.AnimaCurvePushEffect;
        CurrentdirectionView = absCharacterBehaviourController.CurrentDirectionalView;
        CurrentdirectionMove = absCharacterBehaviourController.CurrentDerectionalMove;
        CurrentSpeedRotation = absCharacterBehaviourController.CurrentSpeedRotation;
        CurrentSpeedMove = absCharacterBehaviourController.CurrentSpeedMove;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void SetIsMovableCharacter();
    public abstract void Raning();

}
