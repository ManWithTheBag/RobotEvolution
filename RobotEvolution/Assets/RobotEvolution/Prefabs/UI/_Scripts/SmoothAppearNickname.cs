using UnityEngine;
using TMPro;
using System.Collections;

public class SmoothAppearNickname : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textNecknameScore;
    [SerializeField] private float _timeLearpingCollor;

    private void OnEnable()
    {
        StartCoroutine(LearpingTextCollor());
    }

    private IEnumerator LearpingTextCollor()
    {
        for (float i = 0; i < 1; i += Time.deltaTime / _timeLearpingCollor)
        {
            _textNecknameScore.alpha = i;
            yield return null;
        }
        _textNecknameScore.alpha = 1;
    }

}
