using UnityEngine;

public class PlayerMovement : AbsCharacterMovement
{
    private InputJoystick _inputJoystick;
    private float _relativeAngle;

    public override void Awake()
    {
        TryGetComponent(out InputJoystick inputJoystick); _inputJoystick = inputJoystick;
        base.Awake();
    }

    public override void SetIndividualViewAndMove(AbsCharacterBaseModetState absCharacterBaseModetState)
    {
        _currentBodyView = CulculationQuaternionPlayerView(_inputJoystick.GetDirectionUpdate());
        _currentCharacterMove = _thisTransform.forward;

    }
    
    private Quaternion CulculationQuaternionPlayerView(Vector3 directionView)
    {
        _relativeAngle = Mathf.Atan2(directionView.x, directionView.z) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, _relativeAngle, 0f);
    }
}
