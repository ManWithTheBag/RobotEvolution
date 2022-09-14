using System.Collections.Generic;
using UnityEngine;

public abstract class AbsVeaponShell : AbsVeapon
{

    [SerializeField]private List<Transform> _positionsVeaponShellList;
    private PoolShell _poolShell;

    private void Start()
    {
        if (GameObject.Find("PoolShell").TryGetComponent(out PoolShell poolShell))
            _poolShell = poolShell;
    }

    public override void Shoot(Transform enemyTransform)
    {
        foreach  (Transform item in _positionsVeaponShellList)
        {
            Transform shell = _poolShell.PoolShells.GetFreeElement().transform;
            shell.transform.position = item.position;
            shell.GetComponent<Shell>().LauncheShell(item.forward, _thisTransform.parent);
        }
    }
}
