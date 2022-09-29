using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AbsCharacterMovement : MonoBehaviour
{
    protected NavMeshAgent _navMeshAgent;
    protected Transform _thisTransform;

    private Quaternion _currentTurretView;
    private Transform _turret;
    private CharacterModelStatsDataSO _characterModelStatsDataSO;

    public virtual void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _thisTransform = transform;
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


    public void SetTurretDirectionInUpdate(Quaternion currentTurretView)
    {
        _currentTurretView = currentTurretView;

    }
    public virtual void SetCharacterMovePosition(Vector3 targetPosition)
    {
        _navMeshAgent.SetDestination(targetPosition);
    }

    private void Update()
    {
        RotationBody();
    }

    private void RotationBody()
    {
        _turret.rotation = Quaternion.Lerp(_turret.rotation, _currentTurretView, Time.deltaTime * _characterModelStatsDataSO.SpeedTurretRotation);
    }
}


