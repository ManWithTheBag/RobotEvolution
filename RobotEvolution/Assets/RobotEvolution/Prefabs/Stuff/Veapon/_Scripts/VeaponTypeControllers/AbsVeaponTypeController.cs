using UnityEngine;

public abstract class AbsVeaponTypeController : MonoBehaviour
{
    [SerializeField] protected VeaponDataSO _veaponDataSO;

    protected VeaponWheelBotCannon _veaponWheelBotCannon;
    protected VeaponBigBlaze _veaponBigBlaze;
    protected VeaponPanzerCannon _veaponPanzerCannon;
    protected float _maxAngleShooting;
    protected float _maxDistanceShooting;

    private FiringRangeVisualisate _firingRangeVisualisate;
    private AbsCharacterMovement _absCharacterMovement;


    private void Awake()
    {
        _absCharacterMovement = GetComponent<AbsCharacterMovement>();
        TryGetComponent(out FiringRangeVisualisate firingRangeVisualisate); _firingRangeVisualisate = firingRangeVisualisate;

        AddVeaponComponents();
    }

    public abstract void AddVeaponComponents();
    public abstract void DisableAllVeapons();

    protected void SetDefoltMaxValue()
    {
        _maxDistanceShooting = 0;
        _maxAngleShooting = 0;
    }

    public virtual void CreateWheelBotCannonVeapon(IVeaponSetuper iVeaponSetuper)
    {
        SetMaxDistanceSooting(_veaponDataSO.MaxDistanceWheelBotCannon);
        SetMaxAngleShooting(_veaponDataSO.ViewAngleTurretWheelBotCannon);
    }

    public virtual void CreateBigBlazeVeapon(IVeaponSetuper iVeaponSetuper, LineRenderer lineRenderer)
    {
        SetMaxDistanceSooting(_veaponDataSO.MaxDistanceBigBlaze);
        SetMaxAngleShooting(_veaponDataSO.ViewAngleTurretAndVeaponBigBlaze);
    }

    public virtual void CreatePanzerCannonVeapon(IVeaponSetuper iVeaponSetuper)
    {
        SetMaxDistanceSooting(_veaponDataSO.MaxDistancePanzerCannon);
        SetMaxAngleShooting(_veaponDataSO.ViewAngleTurretPanzerCannon);
    }


    #region Look for max value from earch model
    public virtual void SetupMaxValueVArible()
    {
        _firingRangeVisualisate.SetupVisualisateVarible(_maxDistanceShooting, _maxAngleShooting);

        _absCharacterMovement.SetCurrentMaxAngleViewTarret(_maxAngleShooting);
    }

    protected void SetMaxDistanceSooting(float distanceShooting)
    {
        if (_maxDistanceShooting < distanceShooting)
            _maxDistanceShooting = distanceShooting;
    }

    protected void SetMaxAngleShooting(float angleShooting)
    {
        if (_maxAngleShooting < angleShooting)
            _maxAngleShooting = angleShooting;
    }
    #endregion
}
