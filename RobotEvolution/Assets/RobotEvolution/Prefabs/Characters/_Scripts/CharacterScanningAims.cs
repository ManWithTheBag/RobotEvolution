using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterScanningAims : MonoBehaviour
{
    [SerializeField] private Transform _scannerTransform;
    [Min(0)] [SerializeField] private float _maxScanDistance = 100;
    [Min(0)] [SerializeField] private float _scanningAngleY = 140;
    [Min(0)] [SerializeField] private int _countRayInScanY = 6;
    [Min(0)] [SerializeField] private float _scanningAngleX = 30;
    [Min(0)] [SerializeField] private int _countRayInScanX = 1;

    private List<IDistanceAimsComparable> _aimsList = new();
    private List<string> _tegsForScan = new();
    private DistanceToAimComparer _distanceToAimComparer;

    private Transform _currentHitTransform;
    private RaycastHit _hit;

    private void Awake()
    {
        _distanceToAimComparer = new DistanceToAimComparer();
    }

    public List<IDistanceAimsComparable> GetVisibleSoortedAimsList(List<string> tegsForScan)
    {
        _aimsList.Clear();

        _tegsForScan = tegsForScan;

        return GetAimsList();
    }

    private List<IDistanceAimsComparable> GetAimsList()
    {
        RayToScan();

        foreach (var item in _aimsList)
        {
            item.CalculateDistanceAimToCharacter(_scannerTransform);
        }

        CatchExceptionSortList();

        return _aimsList;
    }
    private void RayToScan()
    {
        float j = 0;
        float f = 0;
        Vector3 dir;

        for (int i = 0; i < _countRayInScanY; i++)
        {
            var xY = Mathf.Sin(j);
            var yY = Mathf.Cos(j);

            j += + (_scanningAngleY / 2) * Mathf.Deg2Rad / _countRayInScanY;

            dir = _scannerTransform.TransformDirection(new Vector3(xY, 0, yY));
            GetRaycast(dir);
                
            if (xY != 0)
            {
                dir = _scannerTransform.TransformDirection(new Vector3(-xY, 0, yY));
                GetRaycast(dir);
            }

            //for (int k = 0; k < _countRayInScanX; k++)
            //{
            //    var zX = Mathf.Sin(f);
            //    var yX = MathF.Cos(f);

            //    f += +(_scanningAngleX / 2) * Mathf.Deg2Rad / _countRayInScanX;

            //    dir = _scannerTransform.TransformDirection(new Vector3(0, zX, yX));
            //    GetRaycast(dir);

            //    if (xY != 0)
            //    {
            //        dir = _scannerTransform.TransformDirection(new Vector3(0, -zX, yX));
            //        GetRaycast(dir);
            //    }
            //}
        }
    }

    private void GetRaycast(Vector3 dir)
    {
        if (Physics.Raycast(_scannerTransform.position, dir, out _hit, _maxScanDistance))
        {
            _currentHitTransform = _hit.collider.transform;
            foreach (var item in _tegsForScan)
            {
                if (_currentHitTransform.tag == item)
                {
                    if (_currentHitTransform.TryGetComponent(out IDistanceAimsComparable iDistceAimsComparable) && _currentHitTransform != transform)
                        _aimsList.Add(iDistceAimsComparable);
                    else if (_currentHitTransform.parent.TryGetComponent(out IDistanceAimsComparable iDistceAimsComparabl))
                        _aimsList.Add(iDistceAimsComparabl);
                }
            }
        }
    }

    private void CatchExceptionSortList()
    {
        try
        {
            _aimsList.Sort(_distanceToAimComparer);
        }
        catch
        {
            _aimsList.Clear();
        }
    }

    private void OnDrawGizmos()
    {
        //float j = 0;
        //float f = 0;
        //Vector3 dir;

        //for (int i = 0; i < _countRayInScanY; i++)
        //{
        //    var xY = Mathf.Sin(j);
        //    var yY = Mathf.Cos(j);

        //    j += +(_scanningAngleY) * Mathf.Deg2Rad / _countRayInScanY;

        //    dir = _scannerTransform.TransformDirection(new Vector3(xY, 0, yY));
        //    GetRaycast(dir);

        //    if (xY != 0)
        //    {
        //        dir = _scannerTransform.TransformDirection(new Vector3(-xY, 0, yY));
        //        GetRaycast(dir);
        //        Gizmos.color = Color.blue;
        //        Gizmos.DrawRay(_scannerTransform.position, dir * _maxScanDistance);
        //    }

        //    for (int k = 0; k < _countRayInScanX; k++)
        //    {
        //        var zX = Mathf.Sin(f);
        //        var yX = MathF.Cos(f);

        //        f += +(_scanningAngleX / 2) * Mathf.Deg2Rad / _countRayInScanX;

        //        dir = _scannerTransform.TransformDirection(new Vector3(0, zX, yX));
        //        GetRaycast(dir);

        //        if (xY != 0)
        //        {
        //            dir = _scannerTransform.TransformDirection(new Vector3(0, -zX, yX));
        //            GetRaycast(dir);
        //        }
        //    }

        float j = 0;
        Vector3 dir;
        Vector3 dir2;

        for (int i = 0; i < _countRayInScanY; i++)
        {
            var z = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += +(_scanningAngleY / 2) * Mathf.Deg2Rad / _countRayInScanY;

            dir = _scannerTransform.TransformDirection(new Vector3(0, z, y));
            dir2 = _scannerTransform.TransformDirection(new Vector3(0, -z, y));

            //if (z != 0)
            //{
            //    dir = _scannerTransform.TransformDirection(new Vector3(0, z, y));
            //}
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_scannerTransform.position, dir * _maxScanDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_scannerTransform.position, dir2 * _maxScanDistance);
        }

        //float j = 0;
        //Vector3 dir;

        //for (int i = 0; i < _countRayInScanY; i++)
        //{
        //    var z = Mathf.Sin(j);
        //    var y = Mathf.Cos(j);

        //    j += +(_scanningAngleY / 2) * Mathf.Deg2Rad / _countRayInScanY;

        //    dir = _scannerTransform.TransformDirection(new Vector3(0, z, y));


        //    if (z != 0)
        //    {
        //        dir = _scannerTransform.TransformDirection(new Vector3(0, -z, y));
        //    }

        //    Gizmos.color = Color.red;
        //    Gizmos.DrawRay(_scannerTransform.position, dir * _maxScanDistance);
        //}

    }
}
