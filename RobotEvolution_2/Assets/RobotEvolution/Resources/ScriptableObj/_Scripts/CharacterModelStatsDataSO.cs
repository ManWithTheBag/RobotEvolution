using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Scriptable Object/CharacterDataSO", order = 51)]
public class CharacterModelStatsDataSO : ScriptableObject
{
    [field: SerializeField]public CharacterModelStatsEnum TypeModelStateCharacter { get; private set; }
    [field: SerializeField]public GameObject PrefabCharacterModel { get; private set; }
    [field: SerializeField]public Vector3 ThirdtVierCameraPosition { get; private set; }
    [field: SerializeField]public Vector3 NicknamePosition { get; private set; }


    #region  Setup Movement
    [Header("Setup Movement")]
    [SerializeField] public int navMeshModelStateID;

    [Min(0)][SerializeField]private float _navSpeedMovement;
    public float NavSpeedMovement
    {
        get { return _navSpeedMovement; }
        private set { _navSpeedMovement = value; }
    }

    [Min(0)][SerializeField]private float _navAngularspeedBody;
    public float NavAngularSpeedBody
    {
        get { return _navAngularspeedBody; }
        private set { _navAngularspeedBody = value; }
    }

    [Min(0)] [SerializeField] private float _navAnguAcceleration;
    public float NavAnguAcceleration
    {
        get { return _navAnguAcceleration; }
        private set { _navAnguAcceleration = value; }
    }

    [Min(0)][SerializeField]private float _speedTurretRotation;
    public float SpeedTurretRotation
    {
        get { return _speedTurretRotation; }
        private set { _speedTurretRotation = value; }
    }

    //[Min(0)][SerializeField]private float _speedBoost;
    //public float SpeedBoost
    //{
    //    get { return _speedBoost; }
    //    private set { _speedBoost = value; }
    //}
    #endregion

    #region Setup Attack
    [Header("Setup Attack")]
    [Min(0)] [SerializeField] private int _maxAmountCrowdEnemy;
    public int MaxAmountCrowdEnemy
    {
        get { return _maxAmountCrowdEnemy; }
        private set { _maxAmountCrowdEnemy = value; }
    }

    [Min(0)] [SerializeField] private int _timeBattle;
    public int TimeBattle
    {
        get { return _timeBattle; }
        private set { _timeBattle = value; }
    }

    [Min(0)] [SerializeField] private int _timeNotBattle;
    public int TimeNotBattle
    {
        get { return _timeNotBattle; }
        private set { _timeNotBattle = value; }
    }
    #endregion

    #region Setup Shield
    [Header("Setup Shield")]
    [Min(0)][SerializeField]private int _maxCapasityEnergyInShield;
    public int MaxCapasityEnergyInShield
    {
        get { return _maxCapasityEnergyInShield; }
        set { _maxCapasityEnergyInShield = value; }
    }

    [Min(0)][SerializeField]private float _diametrSphereShield;
    public float DiametrSphereShield
    {
        get { return _diametrSphereShield; }
        set { _diametrSphereShield = value; }
    }

    [Min(0)][SerializeField]private float _positionSphereShieldY;
    public float PosotionSphereShieldY
    {
        get { return _positionSphereShieldY; }
        set { _positionSphereShieldY = value; }
    }
    #endregion

}
