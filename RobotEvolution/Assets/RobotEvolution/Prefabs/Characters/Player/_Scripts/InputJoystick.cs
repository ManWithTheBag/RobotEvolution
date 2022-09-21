using UnityEngine;

public class InputJoystick : MonoBehaviour
{
    private Joystick _joystick;

    private void Start()
    {
        GameObject.Find("FixedJoystick").TryGetComponent(out Joystick joystick);
        _joystick = joystick;
    }

    public Vector3 GetDirectionUpdate() 
    {
        return new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
    }
}
