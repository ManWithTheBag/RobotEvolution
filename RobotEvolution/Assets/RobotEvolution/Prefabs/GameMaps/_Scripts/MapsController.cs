using UnityEngine;

public class MapsController : MonoBehaviour
{
    [SerializeField] private MeshCollider _meshCollider;

    public float _sqrSizeFiteMap { get; private set; }

    public void Start()
    {
        CheckSizeMap();
    }

    public void CheckSizeMap()
    {
        _sqrSizeFiteMap = Vector3.SqrMagnitude(_meshCollider.bounds.center - _meshCollider.bounds.extents);
    }
}
