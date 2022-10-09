using UnityEngine;

public class InputJoystick : MonoBehaviour
{
    private Joystick _joystick;

    private void Awake()
    {
        GameObject.Find("FixedJoystick").TryGetComponent(out Joystick joystick);
        _joystick = joystick;
    }

    public float GetHorisontalValue()
    {
        return _joystick.Horizontal;
    }

    public float GetVerticalValue ()
    {
        return _joystick.Vertical;
    }
}
