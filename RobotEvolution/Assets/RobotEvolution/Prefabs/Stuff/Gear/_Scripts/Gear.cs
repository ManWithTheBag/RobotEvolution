using UnityEngine;

public class Gear : AbsStuff, IAddScore
{
    [SerializeField] private StuffScoreDataSO _stuffScoreDataSO;

    public int ScoreAdd()
    {
        return _stuffScoreDataSO.GearScore;
    }
}
