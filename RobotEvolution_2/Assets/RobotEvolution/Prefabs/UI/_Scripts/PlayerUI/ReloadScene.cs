using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadScene : MonoBehaviour
{
    [SerializeField] private Button _reloadButton;

    private void OnEnable()
    {
        _reloadButton.onClick.AddListener(OnReloadingScene);
    }
    private void OnDisable()
    {
        _reloadButton.onClick.RemoveListener(OnReloadingScene);
    }

    private void OnReloadingScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
