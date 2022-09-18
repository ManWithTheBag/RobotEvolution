using UnityEngine;
using UnityEngine.UI;

public class ShotFiilB : MonoBehaviour
{
    [SerializeField] private Image _imageShotFill;

    public void SetCurrentValueFilled(float currentFillAmount)
    {
        _imageShotFill.fillAmount = currentFillAmount;
    }
}
