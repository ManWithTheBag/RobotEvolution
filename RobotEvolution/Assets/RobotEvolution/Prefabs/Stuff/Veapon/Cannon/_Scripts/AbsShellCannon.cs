using UnityEngine;

public abstract class AbsShellCannon : AbsStuff, IDamaging
{
    [SerializeField] protected VeaponDataSO _veaponDataSO;
    
    protected Vector3 _directionStuffMoveNorm;
    protected bool _isMove = false;
    protected Transform _souresShot;
    protected float _flightDistance;

    public abstract int ScoreLossTarget();

    public abstract int ScoreAddSoures();
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

    public abstract void ShellCannonMove();
    public abstract void CheckFlightDistanse();
}
