using System.Collections.Generic;
using UnityEngine;

public abstract class AbsVeaponShell : AbsVeapon
{
    protected Transform _turret;
    protected List<Transform> _positionsVeaponShellList;

    private PoolShell _poolShell;
    private ShellCannon _shellClass;

    public override void Awake()
    {
        if (GameObject.Find("PoolShell").TryGetComponent(out PoolShell poolShell))
            _poolShell = poolShell;

        base.Awake();
    }

    public void SetSetupVeaponForModelState(IVeaponSetuper iVeaponSetuper)
    {
        _turret = iVeaponSetuper.Turret;
        _positionsVeaponShellList = iVeaponSetuper.PositionsVeaponList;
    }

    public override void Shoot(Transform enemyTransform)
    {
        _isRecharged = false;
        StartCoroutine(RechargingVeapon());

        foreach  (Transform item in _positionsVeaponShellList)
        {
            Transform shell = _poolShell.PoolShells.GetFreeElement().transform;
            shell.transform.position = item.position;
            _shellClass = shell.GetComponent<ShellCannon>();
            _shellClass.LauncheShell(item.forward);
            _shellClass.SetSouresCharacter(_thisTransform);
        }
    }
}
