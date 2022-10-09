using System.Collections;
using System;
using UnityEngine;

public abstract class AbsCharacterDeath : MonoBehaviour
{
    [SerializeField] private float _timeDie = 2;

    protected IRefreshible _iRefreshible;

    public event Action DeathCharacterEvent;

    private void Awake()
    {
        _iRefreshible = GetComponent<IRefreshible>();
    }

    public void StartDyingCharacter()
    {
        DeathCharacterEvent?.Invoke();

        StartCoroutine(CharacterDying());

    }

    private IEnumerator CharacterDying()
    {
        yield return new WaitForSeconds(_timeDie);

        CharacterDied();
    }

    public virtual void CharacterDied()
    {
        _iRefreshible.TotalReshreshing();
    }
}
