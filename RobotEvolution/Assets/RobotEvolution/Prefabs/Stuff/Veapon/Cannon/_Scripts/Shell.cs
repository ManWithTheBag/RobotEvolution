using UnityEngine;

public class Shell : AbsStuff
{
    [SerializeField] float _speedShellMovement;

    private Vector3 _directionStuffMoveNorm;
    private bool _isCanMove = false;

    public override void SetScore(IDetectable iDetectableEnemy)
    {
        iDetectableEnemy.DetectedLossScore(ScoreDataSO.ScoreVeapoonCannon);

        if (Soures.TryGetComponent(out IDetectable iDetectableSoures))
            iDetectableSoures.DetectedAddScore(ScoreDataSO.ScoreVeapoonCannon);
    }

    public override void TotalReshreshing()
    {
        gameObject.SetActive(false);
    }

    public void LauncheShell(Vector3 directionMove,Transform soures)
    {
        Soures = soures;
        _directionStuffMoveNorm = directionMove.normalized;
        _isCanMove = true;
    }

    private void FixedUpdate()
    {
        if (_isCanMove)
            _rb.MovePosition(_thisTransform.position + (_directionStuffMoveNorm * Time.fixedDeltaTime * _speedShellMovement));
    }
}
