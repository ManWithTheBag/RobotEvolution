using System;
using System.Collections;
using UnityEngine;

public abstract class AbsVeapon : MonoBehaviour
{
    protected VeaponDataSO _veaponDataSO;
    protected float _maxShootDistance;
    protected float _viewAngleTurretAndVeapon;
    protected float _timeRechargeVeapon;
    protected bool _isRecharged = true;
    protected Transform _thisTransform;
    protected CharactersAims _charactersAims;

    private float _currentAngleToEnemy;
    private float _currentDistanceToEnemy;

    public virtual void Awake()
    {
        _thisTransform = transform;
        TryGetComponent(out CharactersAims charactersAims); _charactersAims = charactersAims;
    }

    public void SetFildsVeapon(VeaponDataSO veaponDataSO)
    {
        _veaponDataSO = veaponDataSO;

        _viewAngleTurretAndVeapon = _veaponDataSO.ViewAngleTurretAndVeaponBigBlaze;
        _maxShootDistance = _veaponDataSO.MaxDistanceBigBlaze;
        _timeRechargeVeapon = _veaponDataSO.TimeRechargeBigBlaze;
    }

    public virtual void Update()
    {
        TryToShoot(_charactersAims.NearestEnemy);
    }

    public virtual void TryToShoot(Transform enemyTransform)
    {
        if (_isRecharged)
        {
            FillButtonImage(0);

            _currentAngleToEnemy = Vector3.Angle(_thisTransform.forward, enemyTransform.position - _thisTransform.position);
            _currentDistanceToEnemy = Vector3.Distance(_thisTransform.position, enemyTransform.position);

            if (_currentDistanceToEnemy < _maxShootDistance && _currentAngleToEnemy < _viewAngleTurretAndVeapon / 2f)
                Shoot(enemyTransform);
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

    public abstract void Shoot(Transform enemyTransform);
}
