using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent ActivatePlayerEvent = new();
    public static UnityEvent DeactivatePlayerEvent = new();
    public static UnityEvent SearchNewAimEvent = new();
    public static UnityEvent SwapScoreAnyCharactersEvent = new();
}
