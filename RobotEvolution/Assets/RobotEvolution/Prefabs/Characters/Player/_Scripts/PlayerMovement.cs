using UnityEngine;

public class PlayerMovement : AbsCharacterMovement
{
    private InputJoystick _inputJoystick;
    private float _relativeAngle;
    private Vector3 _movePlayerVector;

    public override void Awake()
    {
        TryGetComponent(out InputJoystick inputJoystick); _inputJoystick = inputJoystick;
        base.Awake();
    }

    public override void SetIndividualViewAndMove(AbsCharacterBaseModetState absCharacterBaseModetState)
    {
        _movePlayerVector = _inputJoystick.GetDirectionUpdate();

        if (_movePlayerVector != Vector3.zero)
        {
            _currentBodyView = CulculationQuaternionPlayerView(_inputJoystick.GetDirectionUpdate());
            _currentCharacterMove = _thisTransform.forward;
        }
        else
            _currentCharacterMove = Vector3.zero;
    }
    
    private Quaternion CulculationQuaternionPlayerView(Vector3 directionView)
    {
        _relativeAngle = Mathf.Atan2(directionView.x, directionView.z) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, _relativeAngle, 0f);
    }
}
