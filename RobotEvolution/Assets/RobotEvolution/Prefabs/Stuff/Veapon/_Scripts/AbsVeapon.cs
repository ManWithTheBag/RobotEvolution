using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbsVeapon : MonoBehaviour, IShootable
{
    protected float _shootDistance;
    protected float _timeRecharge;
    protected bool _isRecharged = true;
    protected Transform _thisTransform;

    public void SetSetupCharacterModelForShoot(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        _thisTransform = transform;
        _shootDistance = characterModelStatsDataSO.ShotDistance;
        _timeRecharge = characterModelStatsDataSO.TimeRechargeCannon;
    }

    public void TryShootUpdate(Transform enemyTransform, float distanceToEnemy)
    {
        if (_isRecharged)
        {
            if (distanceToEnemy < _shootDistance)
            {
                _isRecharged = false;
                Shoot(enemyTransform);

                TestAsync();
            }
        }
    }

    private async Task TestAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(_timeRecharge));
        _isRecharged = true;
    }

    public abstract void Shoot(Transform enemyTransform);

}
