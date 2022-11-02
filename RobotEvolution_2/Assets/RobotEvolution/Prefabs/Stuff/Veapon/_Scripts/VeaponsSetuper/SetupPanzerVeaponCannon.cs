
public class SetupPanzerVeaponCannon : AbsVeaponSetuper
{
    public override void SetupVeaponForModelState()
    {
        _absVeaponTypeController.CreatePanzerCannonVeapon(this);
    }
}
