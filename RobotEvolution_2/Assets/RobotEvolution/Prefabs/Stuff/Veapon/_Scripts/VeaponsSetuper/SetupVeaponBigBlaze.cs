using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SetupVeaponBigBlaze : AbsVeaponSetuper
{
    private LineRenderer _lineRenderer;

    public override void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        base.Awake();
    }

    public override void SetupVeaponForModelState()
    {
        _absVeaponTypeController.CreateBigBlazeVeapon(this, _lineRenderer);
    }
}
