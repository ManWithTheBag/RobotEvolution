using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ModelProgressBarFields : MonoBehaviour
{
    [field: SerializeField] public Image ImageModelProgressBar { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextCurrentScore { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextUpperLimit { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextLowerLImit { get; private set; }
}
