using UnityEngine;

public class WhillBotShellCannon : AbsShellCannon
{
    public override int ScoreLossTarget()
    {
        return _veaponDataSO.ScoreDamageWheelBotCannon;
    }

    public override int ScoreAddSoures()
    {
        return (int)(_veaponDataSO.ScoreDamageWheelBotCannon / _veaponDataSO.ReductionGetScoreFactor);
    }


    public override void ShellCannonMove()
    {
        if (_isMove)
            _thisTransform.Translate(_directionStuffMoveNorm * _veaponDataSO.SpeedShellWheelBotCannon * Time.deltaTime);
    }
    public override void CheckFlightDistanse()
    {
        _flightDistance = Vector3.Distance(_thisTransform.position, _souresShot.position);

        if (_flightDistance > _veaponDataSO.MaxDistanceWheelBotCannon)
            HitToSomeone();
    }
}
