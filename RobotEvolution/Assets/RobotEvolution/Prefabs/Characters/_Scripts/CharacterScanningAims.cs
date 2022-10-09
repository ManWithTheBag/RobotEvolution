using System.Collections.Generic;
using UnityEngine;

public class CharacterScanningAims : MonoBehaviour
{
    [SerializeField] private Transform _scannerTransform;
    [Min(0)] [SerializeField] private float _maxScanDistance = 100;
    [Min(0)] [SerializeField] private float _scanningAngleY = 140;
    [Min(0)] [SerializeField] private int _countRayInScanY = 6;
    [SerializeField] private List<int> _rotationScanerX;

    private List<IDistanceAimsComparable> _aimsList = new();
    private int _bitMask;
    private DistanceToAimComparer _distanceToAimComparer;
    private Transform _currentHitTransform;

    private void Awake()
    {
        _distanceToAimComparer = new DistanceToAimComparer();
    }

    public List<IDistanceAimsComparable> GetVisibleSoortedAimsList(List<int> layerMaskForScanList)
    {
        _aimsList.Clear();

        _bitMask = 0;
        foreach (var item in layerMaskForScanList)
        {
            _bitMask |= (1 << item);
        }

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
        Vector3 dir;
        for (int i = 0; i < _rotationScanerX.Count; i++)
        {
            _scannerTransform.rotation = Quaternion.Euler((float)_rotationScanerX[i], 0f, 0f);

            for (int k = 0; k < _countRayInScanY; k++)
            {
                var z = Mathf.Sin(j);
                var y = Mathf.Cos(j);

                j += +(_scanningAngleY / 2) * Mathf.Deg2Rad / _countRayInScanY;

                dir = _scannerTransform.TransformDirection(new Vector3(z, 0, y));
                GetRaycast(dir);

                if (z != 0)
                {
                    dir = _scannerTransform.TransformDirection(new Vector3(-z, 0, y));
                    GetRaycast(dir);
                }
            }
        }
    }

    private void GetRaycast(Vector3 dir)
    {
        RaycastHit hit;

        if (Physics.Raycast(_scannerTransform.position, dir, out hit, _maxScanDistance, _bitMask))
        {
            _currentHitTransform = hit.collider.transform;

            if (_currentHitTransform.TryGetComponent(out IDistanceAimsComparable iDistceAimsComparable) && _currentHitTransform != transform)
                _aimsList.Add(iDistceAimsComparable);
            else if (_currentHitTransform.parent.TryGetComponent(out IDistanceAimsComparable iDistceAimsComparabl))
                _aimsList.Add(iDistceAimsComparabl);
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
}
