using UnityEngine;

public class InputJoystick : MonoBehaviour
{
    private Joystick _joystick;

    private void Start()
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


    //public Vector3 GetDirectionUpdate() 
    //{
    //    return new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
    //}
}
