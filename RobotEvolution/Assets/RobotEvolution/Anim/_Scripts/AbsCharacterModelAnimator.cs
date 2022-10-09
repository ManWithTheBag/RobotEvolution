using UnityEngine;

public abstract class AbsCharacterModelAnimator : MonoBehaviour
{
    private Animator _characterModelAnimator;
    private void Awake()
    {
        TryGetComponent(out Animator animator); _characterModelAnimator = animator;
    }

    public void PlayIdle()
    {
        _characterModelAnimator.SetTrigger("IdleTrigger");
    }

    public void PlayRun()
    {
        _characterModelAnimator.SetTrigger("RunTrigger");
    }

    public void PlayShot()
    {
        _characterModelAnimator.SetTrigger("ShotTrigger");
    }

}
