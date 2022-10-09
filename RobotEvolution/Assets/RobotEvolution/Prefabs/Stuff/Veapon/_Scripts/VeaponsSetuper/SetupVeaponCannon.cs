using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VeaponUpdater))]
public class SetupVeaponCannon : MonoBehaviour, IVeaponSetuper
{
    [field: SerializeField] public Transform Turret { get; private set; }
    [field: SerializeField] public List<Transform> PositionsVeaponList { get; private set; }

    private AbsVeaponTypeController _absVeaponTypeController;

    private void Awake()
    {
        transform.parent.TryGetComponent(out AbsVeaponTypeController absVeaponTypeController); _absVeaponTypeController = absVeaponTypeController;
    }

    public void SetupVeaponForModelState()
    {
        _absVeaponTypeController.CreateCannonVeapon(this);
    }
}
