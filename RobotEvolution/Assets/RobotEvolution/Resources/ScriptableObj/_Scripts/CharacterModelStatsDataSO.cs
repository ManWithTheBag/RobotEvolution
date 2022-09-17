using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Scriptable Object/CharacterDataSO", order = 51)]
public class CharacterModelStatsDataSO : ScriptableObject
{
    [field: SerializeField] public CharacterModelStatsEnum TypeModelStateCharacter { get; private set; }
    [field: SerializeField] public GameObject PrefabCharacterModel { get; private set; }


    #region  Setup Movement
    [Header("Setup Movement")]
    [Min(0)] [SerializeField] private float _speedMovement;
    public float SpeedMovement
    {
        get { return _speedMovement; }
        private set { _speedMovement = value; }
    }

    [Min(0)] [SerializeField] private float _speedBodyRotation;
    public float SpeedBodyRotation
    {
        get { return _speedBodyRotation; }
        private set { _speedBodyRotation = value; }
    }

    [Min(0)] [SerializeField] private float _speedTurretRotation;
    public float SpeedTurretRotation
    {
        get { return _speedTurretRotation; }
        private set { _speedTurretRotation = value; }
    }

    [Min(0)] [SerializeField] private float _speedBoost;
    public float SpeedBoost
    {
        get { return _speedBoost; }
        private set { _speedBoost = value; }
    }
    #endregion

    #region  Setup Shoot Veapon
    [Header("Setup Shoot Veapon")]
    [Range(0, 360)] [SerializeField] private float _viewAngle;
    public float ViewAngle
    {
        get { return _viewAngle; }
        private set { _viewAngle = value; }
    }

    [Min(0)] [SerializeField] private float _shotDistance;
    public float ShotDistance
    {
        get { return _shotDistance; }
        private set { _shotDistance = value; }
    }

    [Min(0)] [SerializeField] private float _distancePreparedToFire;
    public float DistancePreparedToFire
    {
        get { return _distancePreparedToFire; }
        private set 
        { 
            if(_distancePreparedToFire > _shotDistance)
                _distancePreparedToFire = value; 
            else
            {
                _distancePreparedToFire = -999;
                Debug.LogError($"LoogError: CharacterModelDataSO.DistancePreparedToFire < ShotDistance: {_distancePreparedToFire} < {_shotDistance}");
            }
        }
    }

    [Min(0)] [SerializeField] private float _timeOverheatingGunMachine;
    public float TimeOverheatingGunMachine
    {
        get { return _timeOverheatingGunMachine; }
        private set { _timeOverheatingGunMachine = value; }
    }

    [Min(0)] [SerializeField] private float _timeRechargeCannon;
    public float TimeRechargeCannon
    {
        get { return _timeRechargeCannon; }
        private set { _timeRechargeCannon = value; }
    }
    #endregion

    
    [field: SerializeField]public Vector3 ThirdtVierCameraPosition { get; private set; }
    [field: SerializeField] public Vector3 NicknamePosition { get; private set; }
}
