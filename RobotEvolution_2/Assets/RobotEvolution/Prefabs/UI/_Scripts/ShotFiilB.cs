using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShotFiilB : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _imageShotFill;

    private bool _isButtonHold;

    public bool IsButtonHold
    {
        get { return _isButtonHold; }
        set { _isButtonHold = value; }
    }

    public void SetCurrentValueFilled(float currentFillAmount)
    {
        _imageShotFill.fillAmount = currentFillAmount;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isButtonHold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isButtonHold = false;
    }
}
