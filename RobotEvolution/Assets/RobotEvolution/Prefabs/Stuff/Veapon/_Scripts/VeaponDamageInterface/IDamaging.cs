using UnityEngine;

public interface IDamaging
{
    public int ScoreLossTarget();
    public int ScoreAddSoures();
    public Transform SouresCharacter();
    public void HitToSomeone();
}
