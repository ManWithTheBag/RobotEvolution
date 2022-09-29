using UnityEngine;

public class PlayerMovement : AbsCharacterMovement
{
    private InputJoystick _inputJoystick;
    private Vector3 _directionatMovePlayer;

    public override void Awake()
    {
        TryGetComponent(out InputJoystick inputJoystick); _inputJoystick = inputJoystick;
        base.Awake();
    }

    public override void SetCharacterMovePosition(Vector3 targetPosition)
    {
        _directionatMovePlayer = _inputJoystick.GetDirectionUpdate();

        if (_directionatMovePlayer != Vector3.zero)
        {
            _navMeshAgent.SetDestination(_thisTransform.position + _directionatMovePlayer);
        }
        else
        {
            _navMeshAgent.SetDestination(_thisTransform.position);
        }
    }
}
