using UnityEngine;

public class PanzerShellCannon : AbsShellCannon
{
    public override int ScoreLossTarget()
    {
        return _veaponDataSO.ScoreDamagePanzerCannon;
    }

    public override int ScoreAddSoures()
    {
        return (int)(_veaponDataSO.ScoreDamagePanzerCannon / _veaponDataSO.ReductionGetScoreFactor);
    }


    public override void ShellCannonMove()
    {
        if (_isMove)
            _thisTransform.Translate(_directionStuffMoveNorm * _veaponDataSO.SpeedShellPanzerCannon * Time.deltaTime);
    }
    public override void CheckFlightDistanse()
    {
        _flightDistance = Vector3.Distance(_thisTransform.position, _souresShot.position);

        if (_flightDistance > _veaponDataSO.MaxDistancePanzerCannon)
            HitToSomeone();
    }
}
