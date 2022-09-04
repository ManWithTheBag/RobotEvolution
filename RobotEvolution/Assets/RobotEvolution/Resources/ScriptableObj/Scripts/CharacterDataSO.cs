using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Scriptable Object/CharacterDataSO", order = 51)]
public class CharacterDataSO : ScriptableObject
{
    [field: SerializeField] public CharacterModelStatsEnum TypeModelStateCharacter { get; private set; }
    [field: SerializeField] public GameObject PrefabCharacterModel { get; private set; }

    [Min(0)] [SerializeField] private float _speedMovement;
    public float SpeedMovement
    {
        get { return _speedMovement; }
        private set { _speedMovement = value; }
    }

    [Min(0)] [SerializeField] private float _speedRotation;
    public float SpeedRotation
    {
        get { return _speedRotation; }
        private set { _speedRotation = value; }
    }

    [Min(0)] [SerializeField] private float _speedBoost;
    public float SpeedBoost
    {
        get { return _speedBoost; }
        private set { _speedBoost = value; }
    }
}
