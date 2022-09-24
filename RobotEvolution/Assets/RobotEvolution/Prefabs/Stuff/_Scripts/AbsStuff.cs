using System;
using UnityEngine;

public abstract class AbsStuff : MonoBehaviour, IRefreshible, IDistanceToAimQuikSortable
{
    [Min(0)] [SerializeField] private float _spawnPositionY;
    public float SortDistanceAimToCharacter { get; private set; }
    public Transform SortedTransform { get; private set; }

    protected Transform _thisTransform;

    private RandomPosition _randomPosition;

    public virtual void Awake()
    {
        _thisTransform = transform;
        SortedTransform = _thisTransform;

        _randomPosition = GameObject.Find("ObjController").GetComponent<RandomPosition>();
        _thisTransform.position = _randomPosition.GetRandomPosition(_spawnPositionY);
    }

    public virtual void TotalReshreshing()
    {
        gameObject.SetActive(false);
        transform.position = _randomPosition.GetRandomPosition(_spawnPositionY);
        GlobalEventManager.SearchNewAimEvent.Invoke();
        gameObject.SetActive(true);
    }

    public void CalculateDistanceAimToCharacter(Transform characterTransform)
    {
        SortDistanceAimToCharacter = Vector3.Distance(_thisTransform.position, characterTransform.position);
    }
}
