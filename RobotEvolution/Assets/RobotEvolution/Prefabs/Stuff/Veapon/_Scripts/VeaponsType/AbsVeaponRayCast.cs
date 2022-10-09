using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbsVeaponRayCast : AbsVeapon
{
    [SerializeField] int _countRayInRayCast = 4;
    [SerializeField] float _scanningAngleX = 60;

    protected Transform _turret;
    protected RaycastHit _hit;
    protected List<Transform> _positionsVeaponStartLineRenderList;
    protected Transform _missShotTransform;
    protected Transform _blazeHitTransform;
    
    private LineRenderer _lineRenderer;
    private int _bitEnemyMask;

    public virtual void Start()
    {
        CreateVisualTargetBlaze();
        CreateEnemyBiteMask();
        CreateMissShotTransform();
    }

    private void CreateVisualTargetBlaze()
    {
        _blazeHitTransform = new GameObject("BlazeHitTransform").transform;
    }

    private void CreateEnemyBiteMask()
    {
        int namberLayerModel = LayerMask.NameToLayer("Model");
        int namberLayerSield = LayerMask.NameToLayer("Shield");

        _bitEnemyMask = (1 << namberLayerModel) | (1 << namberLayerSield);
    }

    private void CreateMissShotTransform()
    {
        _missShotTransform = new GameObject("BigBlazeMissClickEnemy").transform;
        _missShotTransform.SetParent(_turret);
        _missShotTransform.localPosition = new Vector3(0, 0, _maxShootDistance * _missShotTransform.localScale.z);
    }

    public void SetSetupVeaponForModelState(IVeaponSetuper iVeaponSetuper, LineRenderer lineRenderer)
    {
        _turret = iVeaponSetuper.Turret;
        _positionsVeaponStartLineRenderList = iVeaponSetuper.PositionsVeaponList;
        _lineRenderer = lineRenderer;

        _lineRenderer.positionCount = 2;
        _lineRenderer.enabled = false;
    }

    public override void Shoot(Transform enemyTransform)
    {
        if (CheckRayCast())
        {
            _isRecharged = false;
            StartCoroutine(RechargingVeapon());

            SetupVisualisatePosition(_hit.point, _hit.transform);
            ChangeScore(_hit.transform);
        }
    }
    protected bool CheckRayCast()
    {
        float j = 0;

        for (int i = 0; i < _countRayInRayCast; i++)
        {
            var z = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += +(_scanningAngleX / 2) * Mathf.Deg2Rad / _countRayInRayCast;

            Vector3 dir = _turret.TransformDirection(new Vector3(0, z, y));
            if (GetRaycast(dir))
                return true;

            if (z != 0)
            {
                dir = _turret.TransformDirection(new Vector3(0, -z, y));
                if (GetRaycast(dir))
                    return true;
            }
        }

        return false;
    }

    private bool GetRaycast(Vector3 dir)
    {
        return Physics.Raycast(_turret.position, dir, out _hit, _maxShootDistance, _bitEnemyMask) ? true : false;
    }


    protected void SetupVisualisatePosition(Vector3 hitPoint, Transform enemyTransform)
    {
        _blazeHitTransform.SetParent(null);
        _blazeHitTransform.position = hitPoint;
        _blazeHitTransform.SetParent(enemyTransform);


        foreach (Transform item in _positionsVeaponStartLineRenderList)
        {
            VisualisateRayCast(item);
        }
    }


    public abstract void ChangeScore(Transform enemyTransform);

    public virtual async Task  VisualisateRayCast(Transform veaponPosition)
    {
        _lineRenderer.enabled = true;

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            _lineRenderer.SetPosition(0, veaponPosition.position);
            _lineRenderer.SetPosition(1, _blazeHitTransform.position);
            await Task.Yield();
        }

        _lineRenderer.enabled = false;
    }
}
