using UnityEngine;
using UnityEngine.UI;

public class UIPanelController : MonoBehaviour
{
    [SerializeField] private Button _startGameB;
    [SerializeField] private GameObject _onlyStartActiveContainerP;
    [SerializeField] private GameObject _onlyGameActiveContainer;
    [SerializeField] private GameObject _poolPlayer;

    private void OnEnable()
    {
        _startGameB.onClick.AddListener(OnStartGame);
        GlobalEventManager.DeactivatePlayerEvent.AddListener(PlayerDesactivated);
    }
    private void OnDisable()
    {
        _startGameB.onClick.RemoveListener(OnStartGame);
        GlobalEventManager.DeactivatePlayerEvent.RemoveListener(PlayerDesactivated);
    }
    private void Start()
    {
        _onlyGameActiveContainer.SetActive(false);
    }

    private void OnStartGame()
    {
        _onlyGameActiveContainer.SetActive(true);
        _onlyStartActiveContainerP.SetActive(false);

        GlobalGameStatus.t_FirstStartGame = false;
        
        if (_poolPlayer.transform.childCount > 0)
            _poolPlayer.transform.GetChild(0).gameObject.SetActive(true);
        else
            Debug.LogError("LoogError: Pool player has 0 child, or not fond PoolPlayer");

        GlobalEventManager.ActivatePlayerEvent.Invoke();
    }

    private void PlayerDesactivated()
    {
        _onlyGameActiveContainer.SetActive(false);
        _onlyStartActiveContainerP.SetActive(true);
    }

}
