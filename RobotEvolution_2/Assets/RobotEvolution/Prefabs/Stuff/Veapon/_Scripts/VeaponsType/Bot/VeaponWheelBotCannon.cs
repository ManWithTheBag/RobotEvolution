
using UnityEngine;

public class VeaponWheelBotCannon : AbsVeaponShell
{
    public override void Awake()
    {
        if (GameObject.Find("PoolWheelBotShell").TryGetComponent(out IPoolUsable poolShell))
            _poolShell = poolShell;
        else
            Debug.LogError($"LogError: Didn't find object whith pool : PoolWheelBotShell");

        base.Awake();
    }

    public override void PlayShotAnimation()
    {
        _characterModelAnimator.PlayShotWheelBotCannon();
    }
}
