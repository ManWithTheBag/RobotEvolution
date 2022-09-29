using UnityEngine;
using UnityEngine.AI;

public class RandomPosition : MonoBehaviour
{
    [SerializeField] private MeshCollider _mapsMeshColider;
    [SerializeField] private Vector3 _sizeCheckColliderBox;
    [SerializeField] private float _chackBoxPosotionY;
    [SerializeField] private float _radiusSearchSphere;

    private NavMeshHit _navMeshHit;
    private float _spawnPositionY;
    private Collider[] hitCollider;
    public Vector3 GetRandomPosition()
    {
        //_spawnPositionY = spawnPositionY;

        if (SearchPositionOnNavMesh())
            return new Vector3(_navMeshHit.position.x, _navMeshHit.position.y, _navMeshHit.position.z);

        Debug.LogError("LoogError: Haven't find free position for spawn");
        return Vector3.zero;
    }

    private bool SearchPositionOnNavMesh()
    {

        for (int i = 0; i < 100; i++)
        {
            if (NavMesh.SamplePosition(Random.insideUnitSphere * _radiusSearchSphere + SearchRandomPosotionOnMap(), out _navMeshHit, _radiusSearchSphere, 1))
                if (CheckMapPoint(_navMeshHit.position))
                    return true;
        }
        Debug.LogError("LogError: Cant find randop position on the NavMesh");
        return false;
    }

    private Vector3 SearchRandomPosotionOnMap()
    {
        float randomX = Random.Range(_mapsMeshColider.bounds.center.x - Random.Range(0, _mapsMeshColider.bounds.extents.x), _mapsMeshColider.bounds.center.x + Random.Range(0, _mapsMeshColider.bounds.extents.x));
        float randomZ = Random.Range(_mapsMeshColider.bounds.center.z - Random.Range(0, _mapsMeshColider.bounds.extents.z), _mapsMeshColider.bounds.center.z + Random.Range(0, _mapsMeshColider.bounds.extents.z));
        return new Vector3(randomX, 0, randomZ);
    }

    private bool CheckMapPoint(Vector3 randomNavPosition)
    {
        hitCollider = Physics.OverlapBox(randomNavPosition, _sizeCheckColliderBox, Quaternion.identity);
        if (hitCollider.Length <= 0)
            return true;
        else
            return false;
    }
}
