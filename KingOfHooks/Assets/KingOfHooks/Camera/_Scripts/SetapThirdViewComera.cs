using Cinemachine;
using System.Collections;
using UnityEngine;

public class SetapThirdViewComera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _thirdPersonViewCamera;
    [SerializeField] private Vector3 _baseFollowOffset;
    [Min(0)] [SerializeField] private float _koefOffsetCamera;
    private CinemachineTransposer _transposer;
    private Vector3 _targetVector;
    private Vector3 _oldVector;

    private void OnEnable()
    {
        GlobalEventManager.OnSwapScalePlayer.AddListener(SetCameraOffset);
    }
    private void OnDisable()
    {
        GlobalEventManager.OnSwapScalePlayer.RemoveListener(SetCameraOffset);
    }

    private void Start()
    {
        _transposer = _thirdPersonViewCamera.GetCinemachineComponent<CinemachineTransposer>();
        _transposer.m_FollowOffset = _baseFollowOffset;
        _oldVector = _baseFollowOffset;
    }

    private void SetCameraOffset(float playerScale)
    {
        _targetVector = new Vector3(_baseFollowOffset.x, _baseFollowOffset.y + playerScale * _koefOffsetCamera, _baseFollowOffset.z - playerScale * _koefOffsetCamera);
        StartCoroutine(LearpFollowOffset());
    }

    private IEnumerator LearpFollowOffset()
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            _transposer.m_FollowOffset = Vector3.Lerp(_oldVector, _targetVector, i);
            yield return null;
        }
        _oldVector = _targetVector;
    }
}
