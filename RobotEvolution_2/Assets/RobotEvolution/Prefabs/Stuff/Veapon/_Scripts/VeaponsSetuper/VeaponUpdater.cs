using UnityEngine;

public class VeaponUpdater : MonoBehaviour
{
    private AbsVeaponTypeController _absVeaponTypeController;
    private IVeaponSetuper[] _iVeaponSetapblesArray;

    private void Awake()
    {
        _absVeaponTypeController = GetComponentInParent<AbsVeaponTypeController>();
        _iVeaponSetapblesArray = GetComponents<IVeaponSetuper>();
    }

    public void EnableModelVeapons()
    {
        foreach (IVeaponSetuper item in _iVeaponSetapblesArray)
        {
            item.SetupVeaponForModelState();
        }

        SetMaxVariblesForModel();
    }

    public void DisableModelVeapons()
    {
        _absVeaponTypeController.DisableAllVeapons();
    }

    private void SetMaxVariblesForModel()
    {
        _absVeaponTypeController.SetupMaxValueVArible();
    }
}
