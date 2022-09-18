using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbsVeapon : MonoBehaviour, IShootable
{
    [SerializeField] protected VeaponDataSO _veaponDataSO;
    public float MaxShootDistance { get; protected set; }
    public float ViewAngleTurretAndVeapon { get; protected set; }
    public bool IsEnemleThisComponent { get; private set; }

    protected float _timeRechargeVeapon;
    protected bool _isRecharged = true;
    protected Transform _thisTransform;
    protected CharactersAims _charactersAims;

    private float _currentAngleToEnemy;
    private float _currentDistanceToEnemy;

    private void OnEnable()
    {
        IsEnemleThisComponent = true;
    }
    private void OnDisable()
    {
        IsEnemleThisComponent = false;
    }

    public virtual void Awake()
    {

        _thisTransform = transform;
        TryGetComponent(out CharactersAims charactersAims); _charactersAims = charactersAims;

        SetupThisVeapon();
    }

    public abstract void SetupThisVeapon();

    public virtual void Update()
    {
        TryToShoot(_charactersAims.NearestAimEnemy);
    }

    public virtual void TryToShoot(Transform enemyTransform)
    {
        if (_isRecharged)
        {
            FillButtonImage(0);

            _currentAngleToEnemy = Vector3.Angle(_thisTransform.forward, enemyTransform.position - _thisTransform.position);
            _currentDistanceToEnemy = Vector3.Distance(_thisTransform.position, enemyTransform.position);

            if (_currentDistanceToEnemy < MaxShootDistance && _currentAngleToEnemy < ViewAngleTurretAndVeapon / 2f)
            {
                _isRecharged = false;
                Shoot(enemyTransform);

                StartCoroutine(RechargingVeapon());
            }
        }
    }

    protected IEnumerator RechargingVeapon()
    {
        for (float i = 0; i < 1; i += Time.deltaTime / _timeRechargeVeapon)
        {
            FillButtonImage(i);
            yield return null;
        }

        _isRecharged = true;
    }

    public virtual void FillButtonImage(float currentFillAmount)
    {

    }
    //public virtual async Task RechargingVeapon()
    //{
    //    await Task.Delay(TimeSpan.FromSeconds(_timeRechargeVeapon));
    //    _isRecharged = true;
    //}

    public abstract void Shoot(Transform enemyTransform);

}
