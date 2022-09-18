using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SetupVeaponBigBlaze : MonoBehaviour, IVeaponSetupble
{
    [SerializeField] private Transform _turret;
    [SerializeField] private List<Transform> _positionsVeaponStartLineRenderList;

    private LineRenderer _lineRenderer;
    private VeaponBigBlaze _veaponBigBlaze;

    private void Awake()
    {
        transform.parent.TryGetComponent(out VeaponBigBlaze veaponBigBlaze); _veaponBigBlaze = veaponBigBlaze;
        _lineRenderer = GetComponent<LineRenderer>();

        SetDisableThisVeapon();
    }
    public void SetEvableThisVeapon()
    {
        _veaponBigBlaze.enabled = true;
    }

    public void SetDisableThisVeapon()
    {
        _veaponBigBlaze.enabled = false;
    }

    public void SetupVeaponForModelState()
    {
        _veaponBigBlaze.SetSetupVeaponForModelState(_positionsVeaponStartLineRenderList, _turret, _lineRenderer);
    }

}
