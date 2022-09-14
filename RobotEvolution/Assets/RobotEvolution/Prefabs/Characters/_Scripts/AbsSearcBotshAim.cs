using System.Collections.Generic;
using UnityEngine;

public abstract class AbsSearcBotshAim : MonoBehaviour
{
    private Transform _aimTransform;
    private Transform _characterTransform;
    private float _sqrDistance;
    protected float _sqrCloseDistance;
    
    public Transform GetNecessaryNearestAim(Transform characterTransform)
    {
        _sqrCloseDistance = MapsController.t_sqrSizeFiteMap;
        _characterTransform = characterTransform;

        SelectSearcingAimLists();

        return _aimTransform;   
    }

    public abstract void SelectSearcingAimLists();
    

    protected void SearchNearestAimInList<T>(List<T> List) where T : MonoBehaviour
    {
        foreach (var item in List)
        {
            _sqrDistance = CalculationSqrDistance(item.gameObject.transform.position);

            if (_sqrCloseDistance > _sqrDistance && item.transform != _characterTransform)
            {
                _sqrCloseDistance = _sqrDistance;
                _aimTransform = item.gameObject.transform;
            }
        }
    }

    private float CalculationSqrDistance(Vector3 aimPosition)
    {
        Vector3 localOffset = aimPosition - _characterTransform.position;
        return localOffset.sqrMagnitude;
    }
}
