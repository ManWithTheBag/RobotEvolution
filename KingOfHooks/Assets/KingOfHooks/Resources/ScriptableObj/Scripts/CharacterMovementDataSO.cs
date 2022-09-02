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

    [Min(0)] [SerializeField] private float _defoltSpeedPoolUp;
    public float DefoltSpeedPoolUp 
    {
        get { return _defoltSpeedPoolUp; }
        private set { _defoltSpeedPoolUp = value; }
    }

    [Min((float) 0.1)] [SerializeField] private float _koefDivideSpeedMotion;
    public float KoefDivideSpeedMotion
    {
        get { return _koefDivideSpeedMotion; }
        private set { _koefDivideSpeedMotion = value; }
    }

    [Min((float)0.1)] [SerializeField] private float _radiusCheckMapSphere;
    public float RadiusCheckMapSphere
    { 
        get { return _radiusCheckMapSphere; }
        private set { _radiusCheckMapSphere = value; }
    }

    [Min(0)] [SerializeField] private float _timeHitMove;
    public float TimeHitMove
    {
        get { return _timeHitMove; }
        private set { _timeHitMove = value; }
    }

    [field: SerializeField] public LayerMask MapLayer { get; private set; }

}
