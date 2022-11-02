using UnityEngine;

public class PlayerMovement : AbsCharacterMovement
{
    private InputJoystick _inputJoystick;
    private float _rotateAngle;
    private bool _isAnimMoved;
    private bool _isFinishedPanzerAnimStartRun = true;

    public override void Awake()
    {
        TryGetComponent(out InputJoystick inputJoystick); _inputJoystick = inputJoystick;
        base.Awake();
    }

    public override void SetupMoveCharacter(CharacterModelStatsDataSO characterModelStatsDataSO, Transform turret, CharacterModelAnimator characterModelAnimator)
    {
        if (_characterModelAnimator != null)
            _characterModelAnimator.FinishedPanzerAnimStartRunEvent -= OnFinishedPanserAnimStartRun;

        base.SetupMoveCharacter(characterModelStatsDataSO, turret, characterModelAnimator);

        _characterModelAnimator.FinishedPanzerAnimStartRunEvent += OnFinishedPanserAnimStartRun;
    }

    private void OnFinishedPanserAnimStartRun(bool isValue)
    {
        _isFinishedPanzerAnimStartRun = isValue;
    }

    public override void SetCharacterMovePosition(Vector3 targetPosition)
    {
        SetMoveAnimation();

        if (_isFinishedPanzerAnimStartRun)
        {
            PlayerMove();
            PlayerRotation();
        }
    }

    private void SetMoveAnimation()
    {
        if (_inputJoystick.GetVerticalValue() == 0 && _isAnimMoved == true)
        {
            _isAnimMoved = false;

            _characterModelAnimator.PlayIdle();
        }
        else if (_inputJoystick.GetVerticalValue() != 0 && _isAnimMoved == false)
        {
            _isAnimMoved = true;

            _characterModelAnimator.PlayRun();
        }
    }

    private void PlayerMove()
    {
        if (_inputJoystick.GetVerticalValue() > 0)
            _navMeshAgent.Move(_thisTransform.forward * _characterModelStatsDataSO.NavSpeedMovement * Time.deltaTime);
        else if (_inputJoystick.GetVerticalValue() < 0)
            _navMeshAgent.Move(-_thisTransform.forward * _characterModelStatsDataSO.NavSpeedMovement * Time.deltaTime);
    }

    private void PlayerRotation()
    {
        if(_characterModelStatsDataSO != null)
            _rotateAngle = _inputJoystick.GetHorisontalValue() * Time.deltaTime * _characterModelStatsDataSO.NavAngularSpeedBody;
        
        _thisTransform.Rotate(0,_rotateAngle, 0);
    }
}
