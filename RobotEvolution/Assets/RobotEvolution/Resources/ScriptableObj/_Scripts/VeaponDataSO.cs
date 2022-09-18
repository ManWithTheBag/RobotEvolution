using UnityEngine;

[CreateAssetMenu(fileName = "VeaponDataSO", menuName = "Scriptable Object/VeaponDataSO", order = 55)]
public class VeaponDataSO : ScriptableObject
{

    [Header("Cannon")]
    [Min(0)] [SerializeField] private int _maxDistanceCannon;
    public int MaxDistanceCannon
    {
        get { return _maxDistanceCannon; }
        set { _maxDistanceCannon = value; }
    }

    [Min(0)] [SerializeField] private int _timeRechargeCannon;
    public int TimeRechargeCannon
    {
        get { return _timeRechargeCannon; }
        set { _timeRechargeCannon = value; }
    }

    [Min(0)] [SerializeField] private int _scoreDamageCannon;
    public int ScoreDamageCannon
    {
        get { return _scoreDamageCannon; }
        private set { _scoreDamageCannon = value; }
    }

    [Range(0, 360)] [SerializeField] private float _viewAngleTurretAndVeaponCannon;
    public float ViewAngleTurretAndVeaponCannon
    {
        get { return _viewAngleTurretAndVeaponCannon; }
        private set { _viewAngleTurretAndVeaponCannon = value; }
    }



    [Header("Big Blaze")]
    [Min(0)] [SerializeField] private int _maxDistanceBigBlaze;
    public int MaxDistanceBigBlaze
    {
        get { return _maxDistanceBigBlaze; }
        set { _maxDistanceBigBlaze = value; }
    }

    [Min(0)] [SerializeField] private int _timeRechargeBigBlaze;
    public int TimeRechargeBigBlaze
    { 
        get { return _timeRechargeBigBlaze; }
        set { _timeRechargeBigBlaze = value; }
    }

    [Min(0)] [SerializeField] private int _scoreDamageBigBlaze;
    public int ScoreDamageBigBlaze
    {
        get { return _scoreDamageBigBlaze; }
        private set { _scoreDamageBigBlaze = value; }
    }

    [Range(0, 360)] [SerializeField] private float _viewAngleTurretAndVeaponBigBlaze;
    public float ViewAngleTurretAndVeaponBigBlaze
    {
        get { return _viewAngleTurretAndVeaponBigBlaze; }
        private set { _viewAngleTurretAndVeaponBigBlaze = value; }
    }


}
