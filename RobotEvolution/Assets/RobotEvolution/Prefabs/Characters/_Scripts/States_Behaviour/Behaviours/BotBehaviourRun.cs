using UnityEngine;

public class BotBehaviourRun : AbsCharacterBaseBehaviour
{
    public BotBehaviourRun(AbsCharacterBehaviourController absCharacterBehaviourController)
        : base(absCharacterBehaviourController)
    {

    }

    public override void Enter()
    {
        IsMovableCharacter = true;
        SetIsMovableCharacter();
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
        _objMovement.GetDirectionMoveAndSpeed(this);
    }
}
