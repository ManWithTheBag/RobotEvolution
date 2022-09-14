using UnityEngine;

public class RandomSpawnStuff : MonoBehaviour
{
    [Min(0)] [SerializeField] private float _spawnPositionY;

    private RandomPosition _randomPosition;

    private void OnEnable()
    {
        _randomPosition = GameObject.Find("ObjController").GetComponent<RandomPosition>(); // TODO: Check perfomence upon complition of development

        GetRandopPosition();
    }

    private void GetRandopPosition()
    {
        transform.position = _randomPosition.GetRandomPosition(_spawnPositionY);
    }
}
