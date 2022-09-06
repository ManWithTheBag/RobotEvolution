using UnityEngine;

public abstract class AbsCharacterBaseModetState : MonoBehaviour
{
    protected AbsCharacterBehaviourController _absCharacterBehaviourController;
    protected CharacterModelStatsDataSO _characterModelStatsDataSO;
    protected CharactersAims _charactersAims;
    public Vector3 CurrentDirectionalView { get; protected set; }
    public Vector3 CurrentDerectionalMove { get; protected set; }

    public abstract void Start();

    public void SetSetapsForModelState(CharacterModelStatsDataSO characterModelStatsDataSO, CharactersAims charactersAims, AbsCharacterBehaviourController absCharacterBehaviourController)
    {
        _characterModelStatsDataSO = characterModelStatsDataSO;
        _absCharacterBehaviourController = absCharacterBehaviourController;
        _charactersAims = charactersAims;
    }

    public virtual void Enter()
    {
        SetapingCharacterBehaviourController();
        gameObject.SetActive(true);
        _absCharacterBehaviourController.SetBehaviourIdle();
    }
    private void SetapingCharacterBehaviourController()
    {
        _absCharacterBehaviourController.SetCurrentBehaviourControllerSetup(_characterModelStatsDataSO, CurrentDirectionalView, CurrentDerectionalMove);
    }

    public virtual void Exit()
    {
        gameObject.SetActive(false);
    }
}
