using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldProgressBarAndButtonFields : MonoBehaviour
{
    [field:SerializeField]public Button ActivateShieldB { get; private set; }
    [field:SerializeField] public Image ImegeFillable { get; private set; }
    [field:SerializeField] public TextMeshProUGUI TestCurrentEnergyValue { get; private set; }
    [field:SerializeField] public TextMeshProUGUI TextUpperLimitEnergy { get; private set; }
    [field:SerializeField] public TextMeshProUGUI TextLowerLimitEnergy { get; private set; }
    
}
