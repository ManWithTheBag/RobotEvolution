using UnityEngine;
using System.Collections;

public class PlayerDeath : AbsCharacterDeath
{
    public override void CharacterDied()
    {
        _iRefreshible.TotalReshreshing();

        StartCoroutine(DisactivaitingPlayer());
    }

    private IEnumerator DisactivaitingPlayer()
    {
        yield return new WaitForEndOfFrame();
        
        gameObject.SetActive(false);
        GlobalEventManager.DeactivatePlayerEvent.Invoke();

    }
}
