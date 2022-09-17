using UnityEngine;
using System.Collections;

public class SetupThirViewCameraPlayer : MonoBehaviour
{
    [SerializeField] private CharacterModelStateSwitcher _characterModelStateSwitcher;

    private Transform _lookAtCameraTransform;
    private Transform _thisTransform;

    private void OnEnable()
    {
        _characterModelStateSwitcher.EnterModelStateEvent += SetSetupThirViewCamera;
    }
    private void OnDisable()
    {
        _characterModelStateSwitcher.EnterModelStateEvent -= SetSetupThirViewCamera;
    }

    private void Awake()
    {
        _thisTransform = transform;
        _lookAtCameraTransform = new GameObject("ThirdViewCameraTransform").transform;
        _lookAtCameraTransform.SetParent(_thisTransform);

        GameObject.Find("CameraController").TryGetComponent(out CameraController cameraController); 
        cameraController.OnSetThirdAndFirstViewCameraTransform(_thisTransform, _lookAtCameraTransform);
    }

    public void SetSetupThirViewCamera(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        StartCoroutine(LearpingThirdViewCameraPosition(characterModelStatsDataSO.ThirdtVierCameraPosition));
    }

    private IEnumerator LearpingThirdViewCameraPosition(Vector3 currentThirdViewCameraPosition)
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            _lookAtCameraTransform.localPosition = Vector3.Lerp(_lookAtCameraTransform.localPosition, currentThirdViewCameraPosition, i);
            yield return null;
        }

        _lookAtCameraTransform.localPosition = currentThirdViewCameraPosition;
    }
}
