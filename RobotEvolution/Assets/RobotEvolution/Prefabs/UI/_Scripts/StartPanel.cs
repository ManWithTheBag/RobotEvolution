using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private Button _startGameB;
    [SerializeField] private GameObject _onlyStartActiveContainerP;
    [SerializeField] private GameObject _onlyGameActiveContainer;
    [SerializeField] private GameObject _poolPlayer;

    private void OnEnable()
    {
        _startGameB.onClick.AddListener(OnStartGame);
    }
    private void OnDisable()
    {
        _startGameB.onClick.RemoveListener(OnStartGame);
    }
    private void Start()
    {
        _onlyGameActiveContainer.SetActive(false);
    }

    private void OnStartGame()
    {
        _onlyGameActiveContainer.SetActive(true);
        _onlyStartActiveContainerP.SetActive(false);
        
        
        if (_poolPlayer.transform.childCount > 0)
            _poolPlayer.transform.GetChild(0).gameObject.SetActive(true);
        else
            Debug.LogError("LoogError: Pool player has 0 child, or not fond PoolPlayer");

        GlobalEventManager.ActivatePlayerEvent.Invoke();
    }

}
