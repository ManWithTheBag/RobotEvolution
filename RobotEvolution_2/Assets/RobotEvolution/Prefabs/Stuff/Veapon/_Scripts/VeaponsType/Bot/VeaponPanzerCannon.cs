using UnityEngine;

public class VeaponPanzerCannon : AbsVeaponShell
{
    public override void Awake()
    {
        if (GameObject.Find("PoolPanzerShell").TryGetComponent(out IPoolUsable poolShell))
            _poolShell = poolShell;
        else
            Debug.LogError($"LogError: Didn't find object whith pool : PoolPanzerShell");
        base.Awake();
    }

    public override void PlayShotAnimation()
    {
        _characterModelAnimator.PlayShotPanzerCannon();
    }
}
