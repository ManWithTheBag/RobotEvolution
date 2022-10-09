using UnityEngine;

public class VeaponTypePlayerController : AbsVeaponTypeController
{
    private PlayerVeaponCannon _playerVeaponCannon;
    private PlayerVeaponBigBlaze _playerVeaponBigBlaze;

    public override void AddVeaponComponents()
    {
        _playerVeaponCannon = gameObject.AddComponent<PlayerVeaponCannon>();
        _playerVeaponBigBlaze = gameObject.AddComponent<PlayerVeaponBigBlaze>();

        DisableAllVeapons();
    }

    public override void DisableAllVeapons()
    {
        _playerVeaponCannon.enabled = false;
        _playerVeaponBigBlaze.enabled = false;

        SetDefoltMaxValue();
    }

    public override void CreateCannonVeapon(IVeaponSetuper iVeaponSetuper)
    {
        _playerVeaponCannon.enabled = true;

        _playerVeaponCannon.SetFildsVeapon(_veaponDataSO);
        _playerVeaponCannon.SetSetupVeaponForModelState(iVeaponSetuper);

        base.CreateCannonVeapon(iVeaponSetuper);
    }


    public override void CreateBigBlazeVeapon(IVeaponSetuper iVeaponSetuper, LineRenderer lineRenderer)
    {
        _playerVeaponBigBlaze.enabled = true;

        _playerVeaponBigBlaze.SetFildsVeapon(_veaponDataSO);
        _playerVeaponBigBlaze.SetSetupVeaponForModelState(iVeaponSetuper, lineRenderer);

        base.CreateBigBlazeVeapon(iVeaponSetuper, lineRenderer);
    }
}
