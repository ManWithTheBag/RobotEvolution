using UnityEngine;
using System;

public class CharacterModelAnimator : MonoBehaviour
{
    private Animator _characterModelAnimator;

    public event Action<bool> FinishedPanzerAnimStartRunEvent;

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

    private void Handle_StartPanzerAnimStartRun()
    {
        FinishedPanzerAnimStartRunEvent?.Invoke(false);
    }
    private void Handle_FinishedPanzerAnimStartRun()
    {
        FinishedPanzerAnimStartRunEvent?.Invoke(true);
    }
}
