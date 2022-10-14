using UnityEngine;

public class VeaponBigBlaze : AbsVeaponRayCast
{
    public override void SetSetupVeaponForModelState(IVeaponSetuper iVeaponSetuper, LineRenderer lineRenderer)
    {
        _turret = iVeaponSetuper.Turret;
        _positionsVeaponStartLineRenderList = iVeaponSetuper.PositionsVeaponList;
        _characterModelAnimator = iVeaponSetuper.CharacterModelAnimator;
        _lineRenderer = lineRenderer;

        _lineRenderer.positionCount = 2;
        _lineRenderer.enabled = false;
    }

    public override void PlayShotAnimation()
    {
        _characterModelAnimator.PlayShotBigBlaze();
    }

    public override void ChangeScore(Transform enemyTransform)
    {
        if(enemyTransform.TryGetComponent(out CharacterChangeScore characterChangeScore))
        {
            characterChangeScore.ScoreChangedLossScore(_veaponDataSO.ScoreDamageBigBlaze);

            _thisTransform.TryGetComponent(out ScoreCalculation scoreCalculation);
            scoreCalculation.AddScore((int)(_veaponDataSO.ScoreDamageBigBlaze / _veaponDataSO.ReductionGetScoreFactor));
        }

        if (enemyTransform.TryGetComponent(out ShieldDetected shieldDetected))
            shieldDetected.InvokeShieldDetectedEvent(_veaponDataSO.ScoreDamageBigBlaze);
    }
}
