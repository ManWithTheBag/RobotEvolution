using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(SphereCollider))]
public class ShieldDetected : MonoBehaviour
{
    private Transform _thisTransform;

    public event Action<int> ShieldDetectedHitEvent;

    private void Awake()
    {
        _thisTransform = transform;   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsTriggerDamagingObj(other, out IDamaging iDamaging))
        {
            if (iDamaging.SouresCharacter() != _thisTransform.parent)
            {
                InvokeShieldDetectedEvent(iDamaging.ScoreLossTarget());
                iDamaging.HitToSomeone();
            }
        }
    }

    private bool IsTriggerDamagingObj(Collider other, out IDamaging iDamaging)
    {
        iDamaging = other.GetComponent<IDamaging>();
        return iDamaging != null;
    }

    public void InvokeShieldDetectedEvent(int lossShieldEnergy)
    {
        ShieldDetectedHitEvent?.Invoke(lossShieldEnergy);
    }
}
