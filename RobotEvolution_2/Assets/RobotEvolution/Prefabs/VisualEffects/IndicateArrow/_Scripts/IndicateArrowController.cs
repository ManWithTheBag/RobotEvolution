using System.Collections;
using UnityEngine;

public class IndicateArrowController : MonoBehaviour
{
    [SerializeField] private float _timeRefreshNearestEnemyList;

    private PoolIndicateArrow _poolIndicateArrow;
    private PlayerAims _playerAims;
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
        Transform perent = _thisTransform;
        while (true)
        {
            if (!perent.TryGetComponent(out PlayerAims playersAims))
                perent = perent.parent;
            else
            {
                _playerAims = playersAims;
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
        for (int i = 0; i < _playerAims.GetNearestEnemyListForIndicationArrow(_poolIndicateArrow.PoolCapacity).Count; i++)
        {
            _poolIndicateArrow.WholeIndicateArrowList[i].SetCurrentEnemy
                (_playerAims.GetNearestEnemyListForIndicationArrow(_poolIndicateArrow.PoolCapacity)[i].SortedTransform);
        }
    }
}
