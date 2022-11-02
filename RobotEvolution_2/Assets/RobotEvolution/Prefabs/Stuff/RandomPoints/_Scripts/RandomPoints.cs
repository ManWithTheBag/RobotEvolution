using UnityEngine;

public class RandomPoints : MonoBehaviour
{
    private Transform _thisTransform;

    private void Awake()
    {
        _thisTransform = transform;
    }

    public Transform GetTransformRandomPoint()
    {
        return _thisTransform;
    }

    public void SetRandomPosition(Vector3 randomPosition)
    {
        transform.position = randomPosition;
    }
}
