using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VeaponUpdater)) ,RequireComponent(typeof(LineRenderer))]
public class SetupVeaponBigBlaze : MonoBehaviour, IVeaponSetuper
{
    [field: SerializeField]public Transform Turret { get; private set; }
    [field: SerializeField]public List<Transform> PositionsVeaponList { get; private set; }

    private LineRenderer _lineRenderer;
    private AbsVeaponTypeController _absVeaponTypeController;

    private void Awake()
    {
        transform.parent.TryGetComponent(out AbsVeaponTypeController absVeaponTypeController); _absVeaponTypeController = absVeaponTypeController;
        _lineRenderer = GetComponent<LineRenderer>();

    }

    public void SetupVeaponForModelState()
    {
        _absVeaponTypeController.CreateBigBlazeVeapon(this, _lineRenderer);
    }
}
