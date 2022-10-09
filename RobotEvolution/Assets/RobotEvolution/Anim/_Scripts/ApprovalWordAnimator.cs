using UnityEngine;

public class ApprovalWordAnimator : MonoBehaviour
{
    [SerializeField] private Animator _approvalWordAnimator;

    public void PlayeApprovalWordAppear()
    {
        _approvalWordAnimator.SetTrigger("AppearTrigger");
    }
    public void PlayApprovalWordHide()
    {
        _approvalWordAnimator.SetTrigger("HideTrigger");
    }
}
