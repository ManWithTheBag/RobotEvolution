using UnityEngine;

public class MapsController : MonoBehaviour
{
    [SerializeField] private MeshCollider _meshCollider;

    public static float t_sqrSizeFiteMap { get; private set; }

    public void Start()
    {
        CheckSizeMap();
    }

    public void CheckSizeMap()
    {
        t_sqrSizeFiteMap = Vector3.SqrMagnitude(_meshCollider.bounds.center - _meshCollider.bounds.extents);
    }
}
