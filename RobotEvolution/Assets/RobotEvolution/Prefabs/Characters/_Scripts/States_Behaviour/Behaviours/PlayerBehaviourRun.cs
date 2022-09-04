
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
        GlobalEventManager.OnSwapScaleCharacters.AddListener(SetSpeedCharacter);

        IsMovableCharacter = true;
        SetIsMovableCharacter();

        SetSpeedCharacter();
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
        CurrentSpeedMovement = _characterDataSO.SpeedMovement;
        CurrentSpeedRotation = _characterDataSO.SpeedMovement;
    }

    public override void Raning()
    {
        CurrentdirectionView = _inputJoystick.SetDirection();
        CurrentdirectionMove = _thisCharacterTransform.forward;

        _objMovement.GetDirectionMoveAndSpeed(this);
    }
}
