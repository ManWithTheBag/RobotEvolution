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

    //#region  Setup Shoot Veapon
    //[Header("Setup Shoot Veapon")]
    //[Range(0, 360)] [SerializeField] private float _viewAngleTurretAndVeapon;
    //public float ViewAngleTurretAndVeapon
    //{
    //    get { return _viewAngleTurretAndVeapon; }
    //    private set { _viewAngleTurretAndVeapon = value; }
    //}
    //#endregion


    [field: SerializeField]public Vector3 ThirdtVierCameraPosition { get; private set; }
    [field: SerializeField] public Vector3 NicknamePosition { get; private set; }
}
