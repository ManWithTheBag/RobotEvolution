
public class Gear : AbsStuff
{
    public override void SetScore(IDetectable iDetectable)
    {
        iDetectable.DetectedAddScore(ScoreDataSO.GearScore);
    }
}
