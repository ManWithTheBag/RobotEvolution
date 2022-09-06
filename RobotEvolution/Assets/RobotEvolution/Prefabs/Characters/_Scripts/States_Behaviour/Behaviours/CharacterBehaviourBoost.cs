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
        IsMovableCharacter = true;
        SetIsMovableCharacter();

        _timeForAnimCurve = 0;
    }

    public override void Exit()
    {
    }

    public override void SetIsMovableCharacter()
    {
        _objMovement.GetIsMovableCharacter(this);
    }

    public override void Raning()
    {
        CurrentSpeedMove *= _animaCurvePushEffect.Evaluate(_timeForAnimCurve += Time.deltaTime);

        _objMovement.GetDirectionMoveAndSpeed(this);
    }
}
