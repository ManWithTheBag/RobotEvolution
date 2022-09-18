using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupVeaponCannon : MonoBehaviour, IVeaponSetupble
{
    [SerializeField] private Transform _turret;
    [SerializeField] private List<Transform> _positionsVeaponShellList;

    private VeaponCannon _veaponCannon;

    private void Awake()
    {
        transform.parent.TryGetComponent(out VeaponCannon veaponCannon); _veaponCannon = veaponCannon;

        SetDisableThisVeapon();
    }
    public void SetEvableThisVeapon()
    {
        _veaponCannon.enabled = true;
    }
    public void SetDisableThisVeapon()
    {
        _veaponCannon.enabled = false;
    }

    public void SetupVeaponForModelState()
    {
        _veaponCannon.SetSetupVeaponForModelState(_positionsVeaponShellList, _turret);
    }
}
