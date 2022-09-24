using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicateArrowSetuper : MonoBehaviour
{
    [Header("Distance to each model")]
    [SerializeField] private float _distanceModelToArrowLivel_1;
    [SerializeField] private float _distanceModelToArrowLivel_2;
    [SerializeField] private float _distanceModelToArrowLivel_3;
    [SerializeField] private float _distanceModelToArrowLivel_4;
    [SerializeField] private float _distanceModelToArrowLivel_5;

    [SerializeField] private PoolIndicateArrow _poolIndicateArrow;

    private CharacterModelStateSwitcher _characterModelStateSwitcher;

    private void Awake()
    {
        _characterModelStateSwitcher = GetComponentInParent<CharacterModelStateSwitcher>();
    }

    private void OnEnable()
    {
        _characterModelStateSwitcher.EnterModelStateEvent += CheckCurrentModelLevel;
    }
    private void OnDisable()
    {
        _characterModelStateSwitcher.EnterModelStateEvent -= CheckCurrentModelLevel;
    } 

    private void CheckCurrentModelLevel(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        switch (((int)characterModelStatsDataSO.TypeModelStateCharacter))
        {
            case ((int)CharacterModelStatsEnum._1_1_WheeledBot):
                SetDistancModelToArrow(_distanceModelToArrowLivel_1);
                break;
            case ((int)CharacterModelStatsEnum._2_1_SpiderBotCrab):
                SetDistancModelToArrow(_distanceModelToArrowLivel_2);
                break;
            case ((int)CharacterModelStatsEnum._2_2_SpiderBotCyclop):
                SetDistancModelToArrow(_distanceModelToArrowLivel_3);
                break;
            case ((int)CharacterModelStatsEnum._2_3_SpiderBotElefant):
                SetDistancModelToArrow(_distanceModelToArrowLivel_4);
                break;
            case ((int)CharacterModelStatsEnum._3_1_Human_1):
                SetDistancModelToArrow(_distanceModelToArrowLivel_5);
                break;
        }
    }

    private void SetDistancModelToArrow(float distance)
    {
        foreach (IndicateArrow item in _poolIndicateArrow.WholeIndicateArrowList)
        {
            item.GetComponent<IndicateArrow>().SetDistanceModelToArrow(distance);
        }
    }
}
