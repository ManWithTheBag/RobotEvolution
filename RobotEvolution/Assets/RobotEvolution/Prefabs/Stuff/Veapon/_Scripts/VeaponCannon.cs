
public class VeaponCannon : AbsVeaponShell
{
    public override void SetupThisVeapon()
    {
        MaxShootDistance = _veaponDataSO.MaxDistanceCannon;
        ViewAngleTurretAndVeapon = _veaponDataSO.ViewAngleTurretAndVeaponCannon;
        _timeRechargeVeapon = _veaponDataSO.TimeRechargeCannon;
    }
}
