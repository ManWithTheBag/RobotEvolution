using UnityEngine;

public class PlayerMovement : AbsCharacterMovement
{
    private InputJoystick _inputJoystick;
    private float _rotateAngle;

    public override void Awake()
    {
        TryGetComponent(out InputJoystick inputJoystick); _inputJoystick = inputJoystick;
        base.Awake();
    }

    public override void SetCharacterMovePosition(Vector3 targetPosition)
    {
        PlayerMove();
        PlayerRotation();
    }

    private void PlayerMove()
    {
        if (_inputJoystick.GetVerticalValue() > 0)
            _navMeshAgent.Move(_thisTransform.forward * _characterModelStatsDataSO.NavSpeedMovement * Time.deltaTime);
        else if (_inputJoystick.GetVerticalValue() < 0)
            _navMeshAgent.Move(-_thisTransform.forward * _characterModelStatsDataSO.NavSpeedMovement * Time.deltaTime);
    }
    int i;
    private void PlayerRotation()
    {
        _rotateAngle = _inputJoystick.GetHorisontalValue() * Time.deltaTime * _characterModelStatsDataSO.NavAngularSpeedBody;
        _thisTransform.Rotate(0,_rotateAngle, 0);
    }
}
