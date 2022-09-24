using UnityEngine;

public interface IDistanceToAimQuikSortable
{
    public Transform SortedTransform { get; }
    public float SortDistanceAimToCharacter { get;}
    public void CalculateDistanceAimToCharacter(Transform characterTransform);
}
