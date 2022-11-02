using System.Collections.Generic;
using UnityEngine;

public abstract class AbsVeaponShell : AbsVeapon
{
    protected IPoolUsable _poolShell;
    protected Transform _turret;
    protected List<Transform> _positionsVeaponShellList;

    private AbsShellCannon _shellClass;

    public void SetSetupVeaponForModelState(IVeaponSetuper iVeaponSetuper)
    {
        _turret = iVeaponSetuper.Turret;
        _positionsVeaponShellList = iVeaponSetuper.PositionsVeaponList;
        _characterModelAnimator = iVeaponSetuper.CharacterModelAnimator;
    }

    public override void Shoot(Transform enemyTransform)
    {
        _isRecharged = false;
        StartCoroutine(RechargingVeapon());

        PlayShotAnimation();

        foreach  (Transform item in _positionsVeaponShellList)
        {
            Transform shell = _poolShell.GetFreeElement().transform;
            shell.transform.position = item.position;
            _shellClass = shell.GetComponent<AbsShellCannon>();
            _shellClass.LauncheShell(item.forward);
            _shellClass.SetSouresCharacter(_thisTransform);
        }
    }
}
