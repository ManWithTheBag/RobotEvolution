using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AbsCharacterMovement : MonoBehaviour
{
    protected CharacterModelStatsDataSO _characterModelStatsDataSO;
    protected CharactersAims _charactersAims;
    protected NavMeshAgent _navMeshAgent;
    protected Transform _thisTransform;

    private AbsCharacterDeath _absCharacterDeath;
    private Transform _turret;
    private Vector3 _turretView;
    private Vector3 _enemyDirection;
    private float _currentAngleToEnemy;
    private float _maxAngleViewTurrt;

    public virtual void Awake()
    {
        _absCharacterDeath = GetComponent<AbsCharacterDeath>();
        _charactersAims = GetComponent<CharactersAims>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _thisTransform = transform;
    }

    private void OnEnable()
    {
        _absCharacterDeath.DeathCharacterEvent += StopCharacter;
    }
    private void OnDisable()
    {
        _absCharacterDeath.DeathCharacterEvent -= StopCharacter;
    }

    public void SetupMoveCharacterOneTime(CharacterModelStatsDataSO characterModelStatsDataSO, Transform turret)
    {
        _characterModelStatsDataSO = characterModelStatsDataSO;
        _turret = turret;

        _navMeshAgent.agentTypeID = characterModelStatsDataSO.navMeshModelStateID;
        _navMeshAgent.speed = characterModelStatsDataSO.NavSpeedMovement;
        _navMeshAgent.angularSpeed = characterModelStatsDataSO.NavAngularSpeedBody;
        _navMeshAgent.acceleration = characterModelStatsDataSO.NavAnguAcceleration;
    }

    public virtual void SetCharacterMovePosition(Vector3 targetPosition)
    {
        _navMeshAgent.SetDestination(targetPosition);
    }
    public void SetCurrentMaxAngleViewTarret(float maxAngleViewTurrt)
    {
        _maxAngleViewTurrt = maxAngleViewTurrt;
    }

    private void Update()
    {
        if (_characterModelStatsDataSO == null)
            return;

        RotationTurret();
    }

    #region Rotation Turret

    private void RotationTurret()
    {
        SetTurretDirectionals();
        _turret.rotation = Quaternion.Lerp(_turret.rotation, Quaternion.LookRotation(_turretView), Time.deltaTime * _characterModelStatsDataSO.SpeedTurretRotation);
    }

    private void SetTurretDirectionals()
    {
        if (CheckCurrentAngleToEnemy() || _charactersAims.NearestEnemy.position == Vector3.zero)
            _turretView = _enemyDirection;
        else 
            _turretView = _thisTransform.forward;
    }

    private bool CheckCurrentAngleToEnemy()
    {
        _enemyDirection = (_charactersAims.NearestEnemy.position - _thisTransform.position);
        _currentAngleToEnemy = Vector3.Angle(_thisTransform.forward, _enemyDirection);

        return (_currentAngleToEnemy < _maxAngleViewTurrt / 2f) ? true : false;
    }
    #endregion

    private void StopCharacter()
    {
        _navMeshAgent.speed = 0;
    }
}


