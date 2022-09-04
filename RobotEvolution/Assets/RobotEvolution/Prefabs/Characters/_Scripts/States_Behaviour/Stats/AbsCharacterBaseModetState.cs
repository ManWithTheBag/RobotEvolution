using UnityEngine;

public abstract class AbsCharacterBaseModetState : MonoBehaviour
{
    protected CharacterDataSO _characterDataSO;
    public Transform _thisTransform;

    public virtual void Start()
    { 

    }

    public void GetCharacterDataSO(CharacterDataSO characterDataSO)
    {
        _characterDataSO = characterDataSO;
    }

    public virtual void Enter()
    {
        gameObject.SetActive(true);
    }
    public virtual void Exit()
    {
        gameObject.SetActive(false);
    }
}
