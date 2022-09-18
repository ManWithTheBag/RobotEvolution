using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbsVeaponRayCast : AbsVeapon
{
    protected Transform _turret;
    protected List<Transform> _positionsVeaponStartLineRenderList;

    private RaycastHit _hit;
    private LineRenderer _lineRenderer;

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
            foreach (Transform item in _positionsVeaponStartLineRenderList)
            {
                VisualisateRayCast(item);
                ChangeScore(enemyTransform);
            }
        }
    }

    private bool CheckRayCast()
    {
        Physics.Raycast(_turret.position, _turret.forward, out _hit);
        if (_hit.collider != null && _hit.collider.tag == "Bot")
            return true;
        else
            return false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawRay(_turret.position, _turret.forward * MaxShootDistance);
    //}

    public abstract void ChangeScore(Transform enemyTransform);

    public virtual async Task  VisualisateRayCast(Transform veaponPosition)
    {
        _lineRenderer.enabled = true;

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            _lineRenderer.SetPosition(0, veaponPosition.position);
            _lineRenderer.SetPosition(1, _hit.point);
            await Task.Yield();
        }

        _lineRenderer.enabled = false;
    }
}
