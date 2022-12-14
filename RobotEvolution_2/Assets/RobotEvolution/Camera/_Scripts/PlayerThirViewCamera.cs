using UnityEngine;
using System.Collections;

public class PlayerThirViewCamera : MonoBehaviour
{
    private CharacterModelStateSwitcher _characterModelStateSwitcher;
    private Transform _lookAtCameraTransform;
    private Transform _thisTransform;

    private void Awake()
    {
        TryGetComponent(out CharacterModelStateSwitcher characterModelStateSwitcher); _characterModelStateSwitcher = characterModelStateSwitcher;

        _thisTransform = transform;

        CreateThirdCameraTransform();

        SetupCameraController();
    }

    private void CreateThirdCameraTransform()
    {
        _lookAtCameraTransform = new GameObject("ThirdViewCameraTransform").transform;
        _lookAtCameraTransform.SetParent(_thisTransform);
        _lookAtCameraTransform.localPosition = Vector3.zero;
    }

    private void SetupCameraController()
    {
        GameObject.Find("CameraController").TryGetComponent(out CameraController cameraController);
        cameraController.OnSetThirdAndFirstViewCameraTransform(_thisTransform, _lookAtCameraTransform);
    }

    private void OnEnable()
    {
        _characterModelStateSwitcher.EnterNewModelStateEvent += OnSetSetupThirViewCamera;
    }
    private void OnDisable()
    {
        _characterModelStateSwitcher.EnterNewModelStateEvent -= OnSetSetupThirViewCamera;
    }


    public void OnSetSetupThirViewCamera(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        StartCoroutine(LearpingThirdViewCameraPosition(characterModelStatsDataSO.ThirdtVierCameraPosition));
    }

    private IEnumerator LearpingThirdViewCameraPosition(Vector3 currentThirdViewCameraPosition)
    {
        if(_lookAtCameraTransform.localPosition == Vector3.zero)
        {
            _lookAtCameraTransform.localPosition = currentThirdViewCameraPosition;
            yield break;
        }


        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            _lookAtCameraTransform.localPosition = Vector3.Lerp(_lookAtCameraTransform.localPosition, currentThirdViewCameraPosition, i);
            yield return null;
        }

        _lookAtCameraTransform.localPosition = currentThirdViewCameraPosition;
    }
}
