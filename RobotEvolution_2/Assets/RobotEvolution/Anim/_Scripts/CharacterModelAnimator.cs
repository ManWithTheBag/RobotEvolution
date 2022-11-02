using UnityEngine;
using System;

public class CharacterModelAnimator : MonoBehaviour
{
    [SerializeField] private Animator _characterModelAnimator;

    public event Action<bool> FinishedPanzerAnimStartRunEvent;

    public void PlayIdle()
    {
        _characterModelAnimator.SetTrigger("IdleTrigger");
    }

    public void PlayRun()
    {
        _characterModelAnimator.SetTrigger("RunTrigger");
    }

    public void PlayShotWheelBotCannon()
    {
        _characterModelAnimator.SetTrigger("ShotCannonTrigger");
    }

    public void PlayShotBigBlaze()
    {
        _characterModelAnimator.SetTrigger("ShotBigBlazeTrigger");
    }

    public void PlayShotPanzerCannon()
    {
        _characterModelAnimator.SetTrigger("ShotPanzerCannonTrigger");
    }

    public void PlayShotGanmachine()
    {
        _characterModelAnimator.SetTrigger("ShotGanmachineTrigger");
    }

    private void Handle_StartPanzerAnimStartRun()
    {
        FinishedPanzerAnimStartRunEvent?.Invoke(false);
    }
    private void Handle_FinishedPanzerAnimStartRun()
    {
        FinishedPanzerAnimStartRunEvent?.Invoke(true);
    }
}
