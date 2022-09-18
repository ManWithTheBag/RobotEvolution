using System.Collections.Generic;
using UnityEngine;

public abstract class AbsVeaponShell : AbsVeapon
{
    protected Transform _turret;

    private List<Transform> _positionsVeaponShellList;
    private PoolShell _poolShell;
    private Shell _shellClass;

    public override void Awake()
    {
        if (GameObject.Find("PoolShell").TryGetComponent(out PoolShell poolShell))
            _poolShell = poolShell;

        base.Awake();
    }

    public void SetSetupVeaponForModelState(List<Transform> positionsVeaponShellList, Transform turret)
    {
        _turret = turret;
        _positionsVeaponShellList = positionsVeaponShellList;
    }

    public override void Shoot(Transform enemyTransform)
    {
        foreach  (Transform item in _positionsVeaponShellList)
        {
            Transform shell = _poolShell.PoolShells.GetFreeElement().transform;
            shell.transform.position = item.position;
            _shellClass = shell.GetComponent<Shell>();
            _shellClass.LauncheShell(item.forward, _thisTransform);
            _shellClass.SetScoreDamageVeapon(_veaponDataSO.ScoreDamageCannon);
        }
    }
}
