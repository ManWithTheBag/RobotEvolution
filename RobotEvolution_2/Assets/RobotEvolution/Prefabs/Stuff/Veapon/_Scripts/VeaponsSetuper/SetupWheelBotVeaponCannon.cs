
public class SetupWheelBotVeaponCannon : AbsVeaponSetuper
{
    public override void SetupVeaponForModelState()
    {
        _absVeaponTypeController.CreateWheelBotCannonVeapon(this);
    }
}
