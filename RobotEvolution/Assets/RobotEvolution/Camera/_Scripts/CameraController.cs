using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _startViewCamera;
    [SerializeField] private CinemachineVirtualCamera _thirdViewPersoneCamera;

    private void OnEnable()
    {
        GlobalEventManager.ActivatePlayerEvent.AddListener(OnSetThirdViewCamera);
        GlobalEventManager.DeactivatePlayerEvent.AddListener(OnSetStartViewCamera);

        CameraSwitcher.Register(_startViewCamera);
        CameraSwitcher.Register(_thirdViewPersoneCamera);
        OnSetStartViewCamera();
    }

    private void OnDisable()
    {
        GlobalEventManager.ActivatePlayerEvent.RemoveListener(OnSetThirdViewCamera);
        GlobalEventManager.DeactivatePlayerEvent.RemoveListener(OnSetStartViewCamera);
    }

    private void OnSetThirdViewCamera()
    {
        CameraSwitcher.SwitchCamera(_thirdViewPersoneCamera);
    }

    private void OnSetStartViewCamera()
    {
        CameraSwitcher.SwitchCamera(_startViewCamera);
    }

    public void OnSetThirdAndFirstViewCameraTransform(Transform characterTransform, Transform lookAtCameraTransform)
    {
        _thirdViewPersoneCamera.LookAt = characterTransform;
        _thirdViewPersoneCamera.Follow = lookAtCameraTransform;
    }
}
