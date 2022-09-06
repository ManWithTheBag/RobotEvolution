using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Scriptable Object/CharacterDataSO", order = 51)]
public class CharacterModelStatsDataSO : ScriptableObject
{
    [field: SerializeField] public CharacterModelStatsEnum TypeModelStateCharacter { get; private set; }
    [field: SerializeField] public GameObject PrefabCharacterModel { get; private set; }

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

    [Min(0)] [SerializeField] private float _speedHeadRotation;
    public float SpeedHeadRotation
    {
        get { return _speedHeadRotation; }
        private set { _speedHeadRotation = value; }
    }

    [Min(0)] [SerializeField] private float _speedBoost;
    public float SpeedBoost
    {
        get { return _speedBoost; }
        private set { _speedBoost = value; }
    }
}
