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
    [Min(0)][SerializeField]private float _speedMovement;
    public float SpeedMovement
    {
        get { return _speedMovement; }
        private set { _speedMovement = value; }
    }

    [Min(0)][SerializeField]private float _speedBodyRotation;
    public float SpeedBodyRotation
    {
        get { return _speedBodyRotation; }
        private set { _speedBodyRotation = value; }
    }

    [Min(0)][SerializeField]private float _speedTurretRotation;
    public float SpeedTurretRotation
    {
        get { return _speedTurretRotation; }
        private set { _speedTurretRotation = value; }
    }

    [Min(0)][SerializeField]private float _speedBoost;
    public float SpeedBoost
    {
        get { return _speedBoost; }
        private set { _speedBoost = value; }
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
