public class BotBehaviourRun : AbsCharacterBaseBehaviour
{
    public BotBehaviourRun(AbsCharacterBehaviourController absCharacterBehaviourController)
        : base(absCharacterBehaviourController)
    {

    }

    public override void Enter()
    {
        GlobalEventManager.OnSwapScaleCharacters.AddListener(SetSpeedCharacter);
        SetSpeedCharacter();

        IsMovableCharacter = true;
        SetIsMovableCharacter();
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
        if (_aimTransform != null)
        {
            CurrentdirectionView = _aimTransform.position - _thisCharacterTransform.position;
            CurrentdirectionMove = _thisCharacterTransform.forward;
        }

        _objMovement.GetDirectionMoveAndSpeed(this);
    }
}
