using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinPoint : MonoBehaviour
{
    [Min(0)][SerializeField] private int _winPointCount;
    public int WinPointsCount
    {
        get { return _winPointCount; }
        set 
        {
            if (value > 0)
                _winPointCount = value;
            else
                Debug.LogError("Log Error: WinPoint can't by negative");
        }
    }

    private CharacterModelStateSwitcher _characterModelStateSwitcher;
    private WinPointT _winPointT;

    private void Awake()
    {
        TryGetComponent(out CharacterModelStateSwitcher characterModelStateSwitcher); _characterModelStateSwitcher = characterModelStateSwitcher;
        _winPointT = GameObject.Find("UiController").GetComponent<WinPointT>();

        RefreshWinPointsText();
    }

    private void OnEnable()
    {
        _characterModelStateSwitcher.FinishFinalModelRateEvent += AddOneWinPoints;
    }
    private void OnDisable()
    {
        _characterModelStateSwitcher.FinishFinalModelRateEvent -= AddOneWinPoints;
    }

    private void AddOneWinPoints()
    {
        _winPointCount++;
        RefreshWinPointsText();
    }

    private void RefreshWinPointsText()
    {
        _winPointT.WinPointsCountT.text = _winPointCount.ToString();
    }
}
