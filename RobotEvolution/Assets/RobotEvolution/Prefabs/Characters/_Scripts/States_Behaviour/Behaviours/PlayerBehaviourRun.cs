
public class PlayerBehaviourRun : AbsCharacterBaseBehaviour
{
    private InputJoystick _inputJoystick;

    public PlayerBehaviourRun(AbsCharacterBehaviourController absCharacterBehaviourController, InputJoystick inputJoystick)
        : base(absCharacterBehaviourController)
    {
        _inputJoystick = inputJoystick;
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
        CurrentdirectionView = _inputJoystick.SetDirection();
        //CurrentdirectionMove = _thisCharacterTransform.forward; TODO: Make for Players control

        _objMovement.GetDirectionMoveAndSpeed(this);
    }
}
