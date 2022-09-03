using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _startViewCamera;
    [SerializeField] private CinemachineVirtualCamera _firstViewPersoneCome;
    [SerializeField] private CinemachineVirtualCamera _thirdViewPersoneCamera;
    [SerializeField] private GameObject _poolPlayer;

    private Transform _playerTransform;
    private void OnEnable()
    {
        GlobalEventManager.OnActivatePlayer.AddListener(OnSetFollowPlayer);
        GlobalEventManager.OnDeactivatePlayer.AddListener(SetStartViewCamera);

        CameraSwitcher.Register(_startViewCamera);
        CameraSwitcher.Register(_firstViewPersoneCome);
        CameraSwitcher.Register(_thirdViewPersoneCamera);
        SetStartViewCamera();
    }

    private void OnDisable()
    {
        GlobalEventManager.OnActivatePlayer.RemoveListener(OnSetFollowPlayer);
        GlobalEventManager.OnDeactivatePlayer.RemoveListener(SetStartViewCamera);
    }

    private void SetStartViewCamera()
    {
        CameraSwitcher.SwitchCamera(_startViewCamera);
    }

    public void OnSwitchCamera()
    {

        if (CameraSwitcher.isActiveCamera(_thirdViewPersoneCamera))
            CameraSwitcher.SwitchCamera(_firstViewPersoneCome);
        else
            CameraSwitcher.SwitchCamera(_thirdViewPersoneCamera);
    }

    public void OnSetFollowPlayer()
    {
        if (_poolPlayer.transform.childCount > 0)
            _playerTransform = _poolPlayer.transform.GetChild(0).transform;

        CameraSwitcher.SwitchCamera(_thirdViewPersoneCamera);
        StartCoroutine(SetCameraData());
    }

    public IEnumerator SetCameraData()
    {
        yield return new WaitForEndOfFrame();
        _thirdViewPersoneCamera.LookAt = _playerTransform;
        _thirdViewPersoneCamera.Follow = _playerTransform;
        _firstViewPersoneCome.LookAt = _playerTransform;
        _firstViewPersoneCome.Follow = _playerTransform;
    }
}
