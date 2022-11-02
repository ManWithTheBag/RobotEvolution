using UnityEngine;

public class VeaponTypePlayerController : AbsVeaponTypeController
{
    private PlayerVeaponWheeledBotCannon _playerVeaponWheeledBotCannon;
    private PlayerVeaponBigBlaze _playerVeaponBigBlaze;
    private PlayerVeaponPanzerCannon _playerVeaponPanzerCannon;

    public override void AddVeaponComponents()
    {
        _playerVeaponWheeledBotCannon = gameObject.AddComponent<PlayerVeaponWheeledBotCannon>();
        _playerVeaponBigBlaze = gameObject.AddComponent<PlayerVeaponBigBlaze>();
        _playerVeaponPanzerCannon = gameObject.AddComponent<PlayerVeaponPanzerCannon>();

        DisableAllVeapons();
    }

    public override void DisableAllVeapons()
    {
        _playerVeaponWheeledBotCannon.enabled = false;
        _playerVeaponBigBlaze.enabled = false;
        _playerVeaponPanzerCannon.enabled = false;

        SetDefoltMaxValue();
    }

    public override void CreateWheelBotCannonVeapon(IVeaponSetuper iVeaponSetuper)
    {
        _playerVeaponWheeledBotCannon.enabled = true;

        _playerVeaponWheeledBotCannon.SetFildsVeapon(_veaponDataSO);
        _playerVeaponWheeledBotCannon.SetSetupVeaponForModelState(iVeaponSetuper);

        base.CreateWheelBotCannonVeapon(iVeaponSetuper);
    }


    public override void CreateBigBlazeVeapon(IVeaponSetuper iVeaponSetuper, LineRenderer lineRenderer)
    {
        _playerVeaponBigBlaze.enabled = true;

        _playerVeaponBigBlaze.SetFildsVeapon(_veaponDataSO);
        _playerVeaponBigBlaze.SetSetupVeaponForModelState(iVeaponSetuper, lineRenderer);

        base.CreateBigBlazeVeapon(iVeaponSetuper, lineRenderer);
    }

    public override void CreatePanzerCannonVeapon(IVeaponSetuper iVeaponSetuper)
    {
        _playerVeaponPanzerCannon.enabled = true;

        _playerVeaponPanzerCannon.SetFildsVeapon(_veaponDataSO);
        _playerVeaponPanzerCannon.SetSetupVeaponForModelState(iVeaponSetuper);

        base.CreatePanzerCannonVeapon(iVeaponSetuper);
    }
}
