using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private Button _startGameB;
    [SerializeField] private GameObject _startP;
    [SerializeField] private GameObject _uiGameContainer;
    [SerializeField] private GameObject _poolPlayer;

    private void OnEnable()
    {
        _startGameB.onClick.AddListener(OnStartGame);
    }
    private void OnDisable()
    {
        _startGameB.onClick.RemoveListener(OnStartGame);
    }

    private void Awake()
    {
        _uiGameContainer.SetActive(false);
    }

    private void OnStartGame()
    {
        _uiGameContainer.SetActive(true);
        _startP.SetActive(false);
        
        
        if (_poolPlayer.transform.childCount > 0)
            _poolPlayer.transform.GetChild(0).gameObject.SetActive(true);
        else
            Debug.LogError("LoogError: Pool player has 0 child, or not fond PoolPlayer");

        GlobalEventManager.ActivatePlayerEvent.Invoke();
    }
}
