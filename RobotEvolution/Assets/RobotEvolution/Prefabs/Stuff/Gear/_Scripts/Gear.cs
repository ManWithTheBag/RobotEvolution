using UnityEngine;

public class Gear : AbsStuff
{
    [SerializeField] private StuffScoreDataSO _stuffScoreDataSO;
    public override void SetScore(IDetectable iDetectable)
    {
        iDetectable.DetectedAddScore(_stuffScoreDataSO.GearScore);
    }
}
