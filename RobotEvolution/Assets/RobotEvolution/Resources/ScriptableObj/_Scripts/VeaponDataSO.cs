using UnityEngine;

[CreateAssetMenu(fileName = "VeaponDataSO", menuName = "Scriptable Object/VeaponDataSO", order = 55)]
public class VeaponDataSO : ScriptableObject
{
    [Header("Common veapon data")]
    [Min(0)] [SerializeField] private float _reductionGetScireFactor;
    public float ReductionGetScoreFactor
    {
        get { return _reductionGetScireFactor; }
        set { _reductionGetScireFactor = value; }
    }



    [Header("WheelBot Cannon")]
    [Min(0)] [SerializeField] private int _maxDistanceWheelBotCannon;
    public int MaxDistanceWheelBotCannon
    {
        get { return _maxDistanceWheelBotCannon; }
        set { _maxDistanceWheelBotCannon = value; }
    }

    [Min(0)] [SerializeField] private int _timeRechargeWheelBotCannon;
    public int TimeRechargeWheelBotCannon
    {
        get { return _timeRechargeWheelBotCannon; }
        set { _timeRechargeWheelBotCannon = value; }
    }
    
    [Min(0)] [SerializeField] private float _speedShellWheelBotCannon;    
    public float SpeedShellWheelBotCannon
    {
        get { return _speedShellWheelBotCannon; }
        set { _speedShellWheelBotCannon = value; }
    }

    [Min(0)] [SerializeField] private int _scoreDamageWheelBotCannon;
    public int ScoreDamageWheelBotCannon
    {
        get { return _scoreDamageWheelBotCannon; }
        private set { _scoreDamageWheelBotCannon = value; }
    }

    [Range(0, 360)] [SerializeField] private float _viewAngleTurretWheelBotCannon;
    public float ViewAngleTurretWheelBotCannon
    {
        get { return _viewAngleTurretWheelBotCannon; }
        private set { _viewAngleTurretWheelBotCannon = value; }
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


    [Header("Panzer Cannon")]
    [Min(0)] [SerializeField] private int _maxDistancePanzerCannon;
    public int MaxDistancePanzerCannon
    {
        get { return _maxDistancePanzerCannon; }
        set { _maxDistancePanzerCannon = value; }
    }

    [Min(0)] [SerializeField] private int _timeRechargePanzerCannon;
    public int TimeRechargePanzerCannon
    {
        get { return _timeRechargePanzerCannon; }
        set { _timeRechargePanzerCannon = value; }
    }

    [Min(0)] [SerializeField] private float _speedShellPanzerCannon;
    public float SpeedShellPanzerCannon
    {
        get { return _speedShellPanzerCannon; }
        set { _speedShellPanzerCannon = value; }
    }

    [Min(0)] [SerializeField] private int _scoreDamagePanzerCannon;
    public int ScoreDamagePanzerCannon
    {
        get { return _scoreDamagePanzerCannon; }
        private set { _scoreDamagePanzerCannon = value; }
    }

    [Range(0, 360)] [SerializeField] private float _viewAngleTurretPanzerCannon;
    public float ViewAngleTurretPanzerCannon
    {
        get { return _viewAngleTurretPanzerCannon; }
        private set { _viewAngleTurretPanzerCannon = value; }
    }
}
