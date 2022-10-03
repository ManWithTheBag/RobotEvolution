using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeaponUpdater : MonoBehaviour
{
    private FiringRangeVisualisate _firingRangeVisualisate;
    private AbsCharacterMovement _absCharacterMovement;

    private IShootable[] _AllIShootableArray;
    private List<IShootable> _EnableIShootableList = new();
    private IVeaponSetupble[] _iVeaponSetapblesArray;
    private float _maxAngleViewTurrt = 0;

    private void Awake()
    {
        _iVeaponSetapblesArray = GetComponents<IVeaponSetupble>();
        _AllIShootableArray = GetComponentsInParent<IShootable>();
        _absCharacterMovement = GetComponentInParent<AbsCharacterMovement>();

        transform.parent.TryGetComponent(out FiringRangeVisualisate firingRangeVisualisate); _firingRangeVisualisate = firingRangeVisualisate;
    }

    public void EnableModelVeapons()
    {
        foreach (IVeaponSetupble item in _iVeaponSetapblesArray)
        {
            item.SetEvableThisVeapon();
            item.SetupVeaponForModelState();
        }

        GetActualIShootableArray();
    }

    public void DisableModelVeapons()
    {
        foreach (IVeaponSetupble item in _iVeaponSetapblesArray)
        {
            item.SetDisableThisVeapon();
        }
    }


    private void GetActualIShootableArray()
    {
        _EnableIShootableList.Clear();

        foreach (IShootable item in _AllIShootableArray)
        {
            if (item.IsEnemleThisComponent)
                _EnableIShootableList.Add(item);
        }

        SetMaxAngleViewTurret();

        SetMaxDistanceFiringRangeVisualisate();
    }
    private void SetMaxAngleViewTurret()
    {
        foreach (var item in _EnableIShootableList)
        {
            if (_maxAngleViewTurrt < item.ViewAngleTurretAndVeapon)
                _maxAngleViewTurrt = item.ViewAngleTurretAndVeapon;
        }

        _absCharacterMovement.SetCurrentMaxAngleViewTarret(_maxAngleViewTurrt);
    }

    private void SetMaxDistanceFiringRangeVisualisate()
    {
        if (_firingRangeVisualisate != null)
            _firingRangeVisualisate.SetMaxDistaceVisualisate(_EnableIShootableList.ToArray());
    }
}
