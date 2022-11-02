using Cinemachine;
using UnityEngine;

public class TurnStartViewCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _startViewCamera;
    [SerializeField] private MeshCollider _meshFloo;
    [SerializeField] private Vector3 _axesRotation;
    [Min(0)] [SerializeField] private float _speedRotation;

    private Transform _thisTransform;
    private void Start()
    {
        _thisTransform = gameObject.transform;
    }

    private void Update()
    {
        if (CameraSwitcher.t_ActiveCmCamera.Equals(_startViewCamera))
            _thisTransform.RotateAround(_meshFloo.bounds.center, _axesRotation, _speedRotation * Time.deltaTime);
    }
}
