using UnityEngine;
[CreateAssetMenu(fileName = "CharacterMovementDataSO", menuName = "Scriptable Object/CharacterMovementDataSO", order = 51)]
public class CharacterMovementDataSO : ScriptableObject
{
    [Min(0)][SerializeField] private float _defoltSpeedMovement;
    public float DefoltSpeedMovement
    {
        get { return _defoltSpeedMovement; }
        private set { _defoltSpeedMovement = value; }
    }

    [Min(0)][SerializeField] private float _defoltSpeedRotation;
    public float DefoltSpeedRotation
    {
        get { return _defoltSpeedRotation; }
        private set { _defoltSpeedRotation = value; }
    }

    [Min(0)] [SerializeField] private float _defoltSpeedPullUp;
    public float DefoltSpeedPullUp 
    {
        get { return _defoltSpeedPullUp; }
        private set { _defoltSpeedPullUp = value; }
    }

    [Min((float) 0.1)] [SerializeField] private float _koefMultSpeedCharacter;
    public float KoefDivideSpeedMotion
    {
        get { return _koefMultSpeedCharacter; }
        private set { _koefMultSpeedCharacter = value; }
    }

    [Min(0)] [SerializeField] private float _timeHitMove;
    public float TimeHitMove
    {
        get { return _timeHitMove; }
        private set { _timeHitMove = value; }
    }
}
