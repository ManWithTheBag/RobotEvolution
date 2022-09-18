using UnityEngine;

public class Shell : AbsStuff
{
    [SerializeField] float _speedShellMovement;

    private Transform _souresShot;
    private Vector3 _directionStuffMoveNorm;
    private bool _isCanMove = false;
    private int _scoreDamageShell;

    public override void SetScore(IDetectable iDetectableEnemy)
    {
        iDetectableEnemy.DetectedLossScore(_scoreDamageShell);

        if (_souresShot.TryGetComponent(out IDetectable iDetectableSoures))
            iDetectableSoures.DetectedAddScore(_scoreDamageShell);
    }

    public override void TotalReshreshing()
    {
        gameObject.SetActive(false);
    }

    public void LauncheShell(Vector3 directionMove,Transform soures)
    {
        _souresShot = soures;
        _directionStuffMoveNorm = directionMove.normalized;
        _isCanMove = true;
    }

    public void SetScoreDamageVeapon(int scoreDamageShell)
    {
        _scoreDamageShell = scoreDamageShell;
    }

    private void FixedUpdate()
    {
        if (_isCanMove)
            _rb.MovePosition(_thisTransform.position + (_directionStuffMoveNorm * Time.fixedDeltaTime * _speedShellMovement));
    }
}
