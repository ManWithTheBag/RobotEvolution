using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    public void SetSetupCharacterModelForShoot(CharacterModelStatsDataSO characterModelStatsDataSO);
    public void TryShootUpdate(Transform enemyTransform, float distanceToEnemy);
}
