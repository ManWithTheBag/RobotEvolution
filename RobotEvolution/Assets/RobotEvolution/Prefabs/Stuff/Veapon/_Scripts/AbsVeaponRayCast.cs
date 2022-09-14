using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AbsVeaponRayCast : AbsVeapon
{
    [SerializeField] private Transform _turret;
    [SerializeField] private ScoreDataSO _scoreDataSO;
    [SerializeField] private List<Transform> _positionsVeaponStartLineRenderList;

    private RaycastHit _hit;
    private LineRenderer _lineRenderer;
    private void Start()
    {
        TryGetComponent(out LineRenderer lineRenderer); _lineRenderer = lineRenderer;
        _lineRenderer.positionCount = 2;
        _lineRenderer.enabled = false;
    }

    public override void Shoot(Transform enemyTransform)
    {
        Physics.Raycast(_turret.position, _turret.forward, out _hit);
        if (_hit.collider == null || _hit.collider.tag != "Character")
            return;

        foreach (Transform item in _positionsVeaponStartLineRenderList)
        {
            VisualisateRayCast(item);
            ChangeScore(enemyTransform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_turret.position, _turret.forward * 10);
    }

    public virtual void ChangeScore(Transform enemyTransform)
    {
        enemyTransform.TryGetComponent(out IDetectable enemyDetectable);
        enemyDetectable.DetectedLossScore(_scoreDataSO.ScoreVeapoonGunMachine);
        _thisTransform.parent.TryGetComponent(out IDetectable thisIDetectable);
        thisIDetectable.DetectedAddScore(_scoreDataSO.ScoreVeapoonGunMachine);
    }

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
