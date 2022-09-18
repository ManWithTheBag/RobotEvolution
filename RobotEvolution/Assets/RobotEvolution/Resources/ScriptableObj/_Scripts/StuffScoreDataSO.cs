using UnityEngine;

[CreateAssetMenu(fileName = "StuffScoreDataSO", menuName = "Scriptable Object/StuffScoreDataSO", order = 53)]
public class StuffScoreDataSO : ScriptableObject
{
    [Min(0)] [SerializeField] private int _gearScore;
    public int GearScore
    {
        get { return _gearScore; }
        private set { _gearScore = value; }
    }
}
