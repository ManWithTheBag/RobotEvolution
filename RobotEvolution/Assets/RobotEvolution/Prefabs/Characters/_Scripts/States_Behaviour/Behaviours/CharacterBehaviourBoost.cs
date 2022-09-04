using UnityEngine;

public class CharacterBehaviourBoost : AbsCharacterBaseBehaviour
{
    private float _timeForAnimCurve;

    public CharacterBehaviourBoost(AbsCharacterBehaviourController absMoveController)
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
        CurrentSpeedMovement = _characterDataSO.SpeedBoost;
        CurrentSpeedRotation = _characterDataSO.SpeedBoost;
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
