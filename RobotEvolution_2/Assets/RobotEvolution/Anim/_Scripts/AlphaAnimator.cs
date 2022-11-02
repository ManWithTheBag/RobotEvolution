using UnityEngine;

public class AlphaAnimator : MonoBehaviour
{
    [SerializeField] private Animator _alphaAnimator;

    public void PlayAppearAlpha()
    {
        _alphaAnimator.SetTrigger("AppearTriger");
    }

    public void PlayHideAlpha()
    {
        _alphaAnimator.SetTrigger("HideTrigger");
    }
}
