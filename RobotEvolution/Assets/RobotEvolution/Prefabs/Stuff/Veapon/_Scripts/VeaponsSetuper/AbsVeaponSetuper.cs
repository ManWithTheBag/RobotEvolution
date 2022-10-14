using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VeaponUpdater))]
public abstract class AbsVeaponSetuper : MonoBehaviour, IVeaponSetuper
{
    [field: SerializeField] public Transform Turret { get; private set; }
    [field: SerializeField] public List<Transform> PositionsVeaponList { get; private set; }
    public CharacterModelAnimator CharacterModelAnimator { get; private set; }

    protected AbsVeaponTypeController _absVeaponTypeController;

    public virtual void Awake()
    {
        transform.parent.TryGetComponent(out AbsVeaponTypeController absVeaponTypeController); _absVeaponTypeController = absVeaponTypeController;
        CharacterModelAnimator = GetComponent<CharacterModelAnimator>();
    }

    public abstract void SetupVeaponForModelState();
}
