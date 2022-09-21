using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbsVeaponRayCast : AbsVeapon
{
    protected Transform _turret;
    protected List<Transform> _positionsVeaponStartLineRenderList;
    protected Transform _missShotTransform;
    protected RaycastHit _hit;

    private LineRenderer _lineRenderer;


    public virtual void Start()
    {
        CreateMissShotTransform();
    }

    private void CreateMissShotTransform()
    {
        _missShotTransform = new GameObject("BigBlazeMissClickEnemy").transform;
        _missShotTransform.SetParent(_turret);
        _missShotTransform.localPosition = new Vector3(0, 0, MaxShootDistance * _missShotTransform.localScale.z);
    }

    public void SetSetupVeaponForModelState(List<Transform> positionsVeaponStartLineRenderList, Transform turret, LineRenderer lineRenderer)
    {
        _positionsVeaponStartLineRenderList = positionsVeaponStartLineRenderList;
        _turret = turret;
        _lineRenderer = lineRenderer;

        _lineRenderer.positionCount = 2;
        _lineRenderer.enabled = false;
    }

    public override void Shoot(Transform enemyTransform)
    {
        if (CheckRayCast())
        {
            SetupVisualisatePosition(_hit.point);
            ChangeScore(_hit.transform);
        }
    }
    protected bool CheckRayCast()
    {
        Physics.Raycast(_turret.position, _turret.forward, out _hit, MaxShootDistance);
        if (_hit.collider != null)
            return true;
        else
            return false;
    }

    protected void SetupVisualisatePosition(Vector3 enemyPosition)
    {
        foreach (Transform item in _positionsVeaponStartLineRenderList)
        {
            VisualisateRayCast(item, enemyPosition);
        }
    }


    public abstract void ChangeScore(Transform enemyTransform);

    public virtual async Task  VisualisateRayCast(Transform veaponPosition, Vector3 enemyPosition)
    {
        _lineRenderer.enabled = true;

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            _lineRenderer.SetPosition(0, veaponPosition.position);
            _lineRenderer.SetPosition(1, enemyPosition);
            await Task.Yield();
        }

        _lineRenderer.enabled = false;
    }
}
