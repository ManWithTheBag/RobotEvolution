using System.Collections.Generic;
using UnityEngine;

public class DetectedAimForPlayer : MonoBehaviour
{
    [SerializeField] private Transform _rayCastDetectTransform;
    [SerializeField] private int _countRayInScan;

    private Transform _enemyTransform;
    private float _searchAngle;
    private float _maxShootDistance;
    private List<Transform> _enemyTransformList = new();

    public Transform SearchPlayerEnemy(float searchAngle, float maxShootDistance)
    {
        _searchAngle = searchAngle;
        _maxShootDistance = maxShootDistance;

        return CheckAimForHook();
    }

    public Transform CheckAimForHook()
    {
        if (RayToScan())
            return SekectNearestEnemy();
        else
            return null;
    }

    private Transform SekectNearestEnemy()
    {
        float temparoryDistance = MapsController.t_sqrSizeFiteMap;
        Transform enemyTransform = null;

        for (int i = 0; i < _enemyTransformList.Count; i++)
        {
            float distanceToEnemy = Vector3.Distance(_rayCastDetectTransform.position, _enemyTransformList[i].position);

            if (temparoryDistance > distanceToEnemy)
            {
                temparoryDistance = distanceToEnemy;
                enemyTransform = _enemyTransformList[i];
            }
        }

        return enemyTransform;
    }

    private bool RayToScan()
    {
        bool result = false;
        bool a = false;
        bool b = false;
        float j = 0;

        for (int i = 0; i < _countRayInScan; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += + (_searchAngle / 2) * Mathf.Deg2Rad / _countRayInScan;

            Vector3 dir = _rayCastDetectTransform.TransformDirection(new Vector3(x, 0, y));
            if (GetRaycast(dir)) 
                
                a = true;
            if (x != 0)
            {
                dir = _rayCastDetectTransform.TransformDirection(new Vector3(-x, 0, y));
                if (GetRaycast(dir)) 
                    b = true;
            }
        }

        if (a || b) result = true;
        return result;
    }

    private bool GetRaycast(Vector3 dir)
    {
        bool result = false;
        RaycastHit hit = new RaycastHit();
        Vector3 position = _rayCastDetectTransform.position;

        if (Physics.Raycast(position, dir, out hit, _maxShootDistance))
        {
            if (hit.collider.tag == "Character")
            {
                _enemyTransformList.Add(hit.transform);
                result = true;
            }
        }

        return result;
    }
}
