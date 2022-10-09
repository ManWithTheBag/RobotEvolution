using UnityEngine;

public abstract class AbsVeaponTypeController : MonoBehaviour
{
    [SerializeField] protected VeaponDataSO _veaponDataSO;
    
    protected VeaponCannon _veaponCannon;
    protected VeaponBigBlaze _veaponBigBlaze;
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

    public virtual void CreateCannonVeapon(IVeaponSetuper iVeaponSetuper)
    {
        SetMaxDistanceSooting(_veaponDataSO.MaxDistanceCannon);
        SetMaxAngleShooting(_veaponDataSO.ViewAngleTurretAndVeaponCannon);
    }

    public virtual void CreateBigBlazeVeapon(IVeaponSetuper iVeaponSetuper, LineRenderer lineRenderer)
    {
        SetMaxDistanceSooting(_veaponDataSO.MaxDistanceBigBlaze);
        SetMaxAngleShooting(_veaponDataSO.ViewAngleTurretAndVeaponBigBlaze);
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
