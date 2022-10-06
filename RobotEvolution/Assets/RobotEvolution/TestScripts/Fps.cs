using UnityEngine;
using TMPro;

public class Fps : MonoBehaviour
{
    private TextMeshProUGUI _textFps;
    private int _fps;

    private void Awake()
    {
        _textFps = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _fps = (int)Mathf.Round(1f / Time.deltaTime);
        _textFps.text = "Fps: " + _fps;
    }
}
