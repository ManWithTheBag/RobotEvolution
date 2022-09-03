using UnityEngine;

public class RandomPosition: MonoBehaviour
{
    [SerializeField] private MeshCollider mapsMeshColider;
    [SerializeField] private Vector3 _sizeCheckColliderBox;

    private float _yPosition;
    private Vector3 _emptyPosition;
    private Collider[] hitCollider;

    public Vector3 GetRandomPosition(float posotionY)
    {
        _yPosition = posotionY;

        for (int i = 0; i < 100; i++)
        {
            if (SearchRandomPosotion())
                return _emptyPosition;
        }
        Debug.LogError("Haven't find free position por spawn");
        return Vector3.zero;
    }

    private bool SearchRandomPosotion()
    {
        float randomX = Random.Range(mapsMeshColider.bounds.center.x - Random.Range(0, mapsMeshColider.bounds.extents.x), mapsMeshColider.bounds.center.x + Random.Range(0, mapsMeshColider.bounds.extents.x));
        float randomZ = Random.Range(mapsMeshColider.bounds.center.z - Random.Range(0, mapsMeshColider.bounds.extents.z), mapsMeshColider.bounds.center.z + Random.Range(0, mapsMeshColider.bounds.extents.z));
        return CheckSpawnPoint(randomX, randomZ);
    }

    private bool CheckSpawnPoint(float randomX, float randomZ)
    {
        Vector3 posOverLapBox = new Vector3(randomX, _yPosition, randomZ);
        hitCollider = Physics.OverlapBox(posOverLapBox, _sizeCheckColliderBox, Quaternion.identity);
        if (hitCollider.Length <= 0)
        {
            _emptyPosition = posOverLapBox;
            return true;
        }
        else
            return false;
    }
}
