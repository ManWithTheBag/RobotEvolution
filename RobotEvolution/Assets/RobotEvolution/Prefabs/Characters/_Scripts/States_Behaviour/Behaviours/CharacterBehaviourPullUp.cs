using UnityEngine;

public class CharacterBehaviourPullUp : AbsCharacterBaseBehaviour
{
    private float _timeForAnimCurve;

    public CharacterBehaviourPullUp(AbsCharacterBehaviourController absMoveController)
     : base(absMoveController)
    {

    }

    public override void Enter()
    {
        GlobalEventManager.OnSwapScaleCharacters.AddListener(SetSpeedCharacter);
        SetSpeedCharacter();

        IsMovableCharacter = true;
        SetIsMovableCharacter();

        _timeForAnimCurve = 0;
    }

    public override void Exit()
    {
        GlobalEventManager.OnSwapScaleCharacters.RemoveListener(SetSpeedCharacter);
    }

    public override void SetIsMovableCharacter()
    {
        _objMovement.GetIsMovableCharacter(this);
    }

    public override void SetSpeedCharacter()
    {
        CurrentSpeedMovement = _characterMovementDataSO.DefoltSpeedPullUp - (_iCharacter.Scale * _characterMovementDataSO.KoefDivideSpeedMotion);
        CurrentSpeedRotation = _characterMovementDataSO.DefoltSpeedMovement - (_iCharacter.Scale * _characterMovementDataSO.KoefDivideSpeedMotion);
    }

    public override void Raning()
    {
        if (_aimTransform != null)
        {
            CurrentdirectionView = _aimTransform.position - _thisCharacterTransform.position;
            CurrentdirectionMove = _thisCharacterTransform.forward;
            CurrentSpeedMovement *= _animaCurvePushEffect.Evaluate(_timeForAnimCurve += Time.deltaTime);
        }

        _objMovement.GetDirectionMoveAndSpeed(this);
    }
}
