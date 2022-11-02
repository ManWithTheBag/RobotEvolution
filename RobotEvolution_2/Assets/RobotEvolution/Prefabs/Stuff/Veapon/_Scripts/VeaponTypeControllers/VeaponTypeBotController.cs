using UnityEngine;

public class VeaponTypeBotController : AbsVeaponTypeController
{
    public override void AddVeaponComponents()
    {
        _veaponWheelBotCannon = gameObject.AddComponent<VeaponWheelBotCannon>();
        _veaponBigBlaze = gameObject.AddComponent<VeaponBigBlaze>();
        _veaponPanzerCannon = gameObject.AddComponent<VeaponPanzerCannon>();

        DisableAllVeapons();
    }
    public override void DisableAllVeapons()
    {
        _veaponWheelBotCannon.enabled = false;
        _veaponBigBlaze.enabled = false;
        _veaponPanzerCannon.enabled = false;

        SetDefoltMaxValue();
    }

    public override void CreateWheelBotCannonVeapon(IVeaponSetuper iVeaponSetuper)
    {
        _veaponWheelBotCannon.enabled = true;

        _veaponWheelBotCannon.SetFildsVeapon(_veaponDataSO);
        _veaponWheelBotCannon.SetSetupVeaponForModelState(iVeaponSetuper);

        base.CreateWheelBotCannonVeapon(iVeaponSetuper);
    }

    public override void CreateBigBlazeVeapon(IVeaponSetuper iVeaponSetuper, LineRenderer lineRenderer)
    {
        _veaponBigBlaze.enabled = true;

        _veaponBigBlaze.SetFildsVeapon(_veaponDataSO);
        _veaponBigBlaze.SetSetupVeaponForModelState(iVeaponSetuper, lineRenderer);

        base.CreateBigBlazeVeapon(iVeaponSetuper, lineRenderer);
    }

    public override void CreatePanzerCannonVeapon(IVeaponSetuper iVeaponSetuper)
    {
        _veaponPanzerCannon.enabled = true;

        _veaponPanzerCannon.SetFildsVeapon(_veaponDataSO);
        _veaponPanzerCannon.SetSetupVeaponForModelState(iVeaponSetuper);

        base.CreateWheelBotCannonVeapon(iVeaponSetuper);
    }
}
