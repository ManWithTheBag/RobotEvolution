
public class CharacterBehaviourDeath : AbsCharacterBaseBehaviour
{
    public CharacterBehaviourDeath(AbsCharacterBehaviourController absMoveController)
     : base(absMoveController)
    {

    }

    public override void Enter()
    {
        IsMovableCharacter = false;
        SetIsMovableCharacter();
    }

    public override void Exit()
    {
    }

    public override void SetSpeedCharacter()
    {
    }

    public override void SetIsMovableCharacter()
    {
        _objMovement.GetIsMovableCharacter(this);
    }

    public override void Raning()
    {
    }
}
