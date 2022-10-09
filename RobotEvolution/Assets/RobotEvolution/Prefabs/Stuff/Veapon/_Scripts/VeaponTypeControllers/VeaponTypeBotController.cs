using UnityEngine;

public class VeaponTypeBotController : AbsVeaponTypeController
{
    public override void AddVeaponComponents()
    {
        _veaponCannon = gameObject.AddComponent<VeaponCannon>();
        _veaponBigBlaze = gameObject.AddComponent<VeaponBigBlaze>();

        DisableAllVeapons();
    }
    public override void DisableAllVeapons()
    {
        _veaponCannon.enabled = false;
        _veaponBigBlaze.enabled = false;

        SetDefoltMaxValue();
    }

    public override void CreateCannonVeapon(IVeaponSetuper iVeaponSetuper)
    {
        _veaponCannon.enabled = true;

        _veaponCannon.SetFildsVeapon(_veaponDataSO);
        _veaponCannon.SetSetupVeaponForModelState(iVeaponSetuper);

        base.CreateCannonVeapon(iVeaponSetuper);
    }

    public override void CreateBigBlazeVeapon(IVeaponSetuper iVeaponSetuper, LineRenderer lineRenderer)
    {
        _veaponBigBlaze.enabled = true;

        _veaponBigBlaze.SetFildsVeapon(_veaponDataSO);
        _veaponBigBlaze.SetSetupVeaponForModelState(iVeaponSetuper, lineRenderer);

        base.CreateBigBlazeVeapon(iVeaponSetuper, lineRenderer);
    }
}
