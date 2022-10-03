using UnityEngine;

public interface IDistanceAimsComparable
{
    public Transform SortedTransform { get; }
    public float SortDistanceAimToCharacter { get;}
    public void CalculateDistanceAimToCharacter(Transform characterTransform);
}
