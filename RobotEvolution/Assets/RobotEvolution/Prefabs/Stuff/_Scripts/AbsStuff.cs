using System;
using UnityEngine;

[SelectionBase]
public abstract class AbsStuff : MonoBehaviour, IRefreshible, IDistanceAimsComparable
{
    public float SortDistanceAimToCharacter { get; private set; }
    public Transform SortedTransform { get; private set; }

    protected Transform _thisTransform;
    private RandomPosition _randomPosition;
    
    public virtual void Awake()
    {
        _thisTransform = transform;
        SortedTransform = _thisTransform;

        _randomPosition = GameObject.Find("ObjController").GetComponent<RandomPosition>();
        _thisTransform.position = _randomPosition.GetRandomPosition();
    }

    public virtual void TotalReshreshing()
    {
        gameObject.SetActive(false);

        transform.position = _randomPosition.GetRandomPosition();
        GlobalEventManager.SearchNewAimEvent.Invoke();

        gameObject.SetActive(true);
    }

    public void CalculateDistanceAimToCharacter(Transform characterTransform)
    {
        SortDistanceAimToCharacter = Vector3.Distance(_thisTransform.position, characterTransform.position);
    }
}
