using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IndicateArrowColorController : MonoBehaviour
{
    [SerializeField] Image _imageIndicateArrow;
    [SerializeField] private Color _minDictanceColor;
    [SerializeField] private Color _maxDistanceColor;

    private void Awake()
    {
        _imageIndicateArrow = GetComponent<Image>();
    }
    private void OnEnable()
    {
        _imageIndicateArrow.color = _maxDistanceColor;
    }

    public void SetCurrentColorDistance(float colorLearpValue)
    {
        if (colorLearpValue < 0.05)
            colorLearpValue = 0;

        _imageIndicateArrow.color = Color.Lerp(_minDictanceColor, _maxDistanceColor, colorLearpValue);
    }
}
