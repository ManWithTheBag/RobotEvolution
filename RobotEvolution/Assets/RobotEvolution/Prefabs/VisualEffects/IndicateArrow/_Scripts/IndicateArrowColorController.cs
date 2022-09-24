using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IndicateArrowColorController : MonoBehaviour
{
    [SerializeField] private Color _minDictanceColor;
    [SerializeField] private Color _maxDistanceColor;

    private Image _imageIndicateArrow;

    private void Awake()
    {
        _imageIndicateArrow = GetComponent<Image>();
    }
    private void Start()
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
