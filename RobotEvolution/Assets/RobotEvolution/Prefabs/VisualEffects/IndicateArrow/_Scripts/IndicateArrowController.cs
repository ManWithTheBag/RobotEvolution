using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicateArrowController : MonoBehaviour
{
    [SerializeField] private float _timeRefreshNearestEnemyList;

    private PoolIndicateArrow _poolIndicateArrow;
    private CharactersAims _charactersAims;
    private Transform _thisTransform;
    private float _timerSearchNewNearestEnemy;

    private void Awake()
    {
        _thisTransform = transform;
        TryGetComponent(out PoolIndicateArrow poolIndicateArrow); _poolIndicateArrow = poolIndicateArrow;

        GetLinkCharacterAimClass();
    }
    private void GetLinkCharacterAimClass()
    {
        Transform perent = _thisTransform.parent;
        while (true)
        {
            if (!perent.TryGetComponent(out CharactersAims charactersAims))
                perent = perent.parent;
            else
            {
                _charactersAims = charactersAims;
                break;
            }
        }
    }


    private void Update()
    {
        SearchNewNearestEnemyTimer();
    }

    private void SearchNewNearestEnemyTimer()
    {
        _timerSearchNewNearestEnemy += Time.deltaTime / _timeRefreshNearestEnemyList;
        if (_timerSearchNewNearestEnemy > 1)
        {
            _timerSearchNewNearestEnemy = 0;
            SetEnemyTransformToIndicateArrow();
        }
    }

    private void SetEnemyTransformToIndicateArrow()
    {
        for (int i = 0; i < _charactersAims.GetNearestEnemyListForIndicationArrow(_poolIndicateArrow.PoolCapacity).Count; i++)
        {
            _poolIndicateArrow.WholeIndicateArrowList[i].SetCurrentEnemy
                (_charactersAims.GetNearestEnemyListForIndicationArrow(_poolIndicateArrow.PoolCapacity)[i].SortedTransform);
        }
    }
}
