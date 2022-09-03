using UnityEngine;

public class InputJoystick : MonoBehaviour
{
    private Joystick _joystick;

    private void Start()
    {
        GameObject.Find("DynamicJoystick").TryGetComponent(out Joystick joystick);
        _joystick = joystick;
    }

    public Vector3 SetDirection() // in Update
    {
        return new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
    }
}
