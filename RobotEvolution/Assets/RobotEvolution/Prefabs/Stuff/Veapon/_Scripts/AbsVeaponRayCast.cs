using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbsVeaponRayCast : AbsVeapon
{
    [SerializeField] int _countRayInRayCast = 3;
    [SerializeField] float _scanningAngle = 60;

    protected Transform _turret;
    protected RaycastHit _hit;
    protected List<Transform> _positionsVeaponStartLineRenderList;
    protected Transform _missShotTransform;
    protected Transform _blazeHitTransform;

    private LineRenderer _lineRenderer;
    private List<int> _hitableLayerMaskList = new();

    public virtual void Start()
    {
        _blazeHitTransform = new GameObject("BlazeHitTransform").transform;

        CreateHilableLayerMask();
        CreateMissShotTransform();
    }

    private void CreateHilableLayerMask()
    {
        _hitableLayerMaskList.Add(LayerMask.NameToLayer("Model"));
        _hitableLayerMaskList.Add(LayerMask.NameToLayer("Shield"));
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

            j += +(_scanningAngle / 2) * Mathf.Deg2Rad / _countRayInRayCast;

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
        for (int i = 0; i < _hitableLayerMaskList.Count; i++)
        {
            if (Physics.Raycast(_turret.position, dir, out _hit, MaxShootDistance))
            {
                return true;
            }
        }

        return false;
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
