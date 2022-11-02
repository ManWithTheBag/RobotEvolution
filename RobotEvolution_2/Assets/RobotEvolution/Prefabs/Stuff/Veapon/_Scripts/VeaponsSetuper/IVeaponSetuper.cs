using System.Collections.Generic;
using UnityEngine;
public interface IVeaponSetuper 
{
    public Transform Turret { get; }
    public List<Transform> PositionsVeaponList { get; }
    public CharacterModelAnimator CharacterModelAnimator { get; }
    public void SetupVeaponForModelState();
}
