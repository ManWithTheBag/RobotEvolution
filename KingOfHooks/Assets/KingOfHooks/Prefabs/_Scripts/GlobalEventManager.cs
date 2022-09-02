using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent OnActivatePlayer = new();
    public static UnityEvent OnDeactivatePlayer = new();
    public static UnityEvent<float> OnSwapScalePlayer = new();
    public static UnityEvent OnSwapScaleCharacters = new();
    public static UnityEvent OnSearchNewAim = new();
}
