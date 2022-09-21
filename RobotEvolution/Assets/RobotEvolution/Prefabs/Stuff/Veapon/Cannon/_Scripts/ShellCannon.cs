using UnityEngine;

public class ShellCannon : AbsStuff, IDamaging
{
    [SerializeField] VeaponDataSO _veaponDataSO;

    private Transform _souresShot;
    private Vector3 _directionStuffMoveNorm;
    private bool _isMove = false;
    private float _flightDistance;
    protected Rigidbody _rb;

    public override void Awake()
    {
        TryGetComponent(out Rigidbody rigidbody); _rb = rigidbody;
        base.Awake();
    }

    public int ScoreLossTarget()
    {
        return _veaponDataSO.ScoreDamageCannon;
    }

    public int ScoreAddSoures()
    {
        return (int)(_veaponDataSO.ScoreDamageCannon / _veaponDataSO.ReductionGetScoreFactor);
    }
    public Transform SouresCharacter()
    {
        return _souresShot;
    }
    public void HitToSomeone()
    {
        _isMove = false;
        TotalReshreshing();
    }


    public override void TotalReshreshing()
    {
        gameObject.SetActive(false);
    }

    public void SetSouresCharacter(Transform soures)
    {
        _souresShot = soures;
    }

    public void LauncheShell(Vector3 directionMove)
    {
        _directionStuffMoveNorm = directionMove.normalized;
        _isMove = true;
    }

    private void Update()
    {
        ShellCannonMove();
        CheckFlightDistanse();
    }

    private void ShellCannonMove()
    {
        if (_isMove)
            _thisTransform.Translate(_directionStuffMoveNorm * _veaponDataSO.SpeedShellCannon * Time.deltaTime);
    }
    private void CheckFlightDistanse()
    {
        _flightDistance = Vector3.Distance(_thisTransform.position, _souresShot.position);

        if (_flightDistance > _veaponDataSO.MaxDistanceCannon)
            HitToSomeone();
    }
}
