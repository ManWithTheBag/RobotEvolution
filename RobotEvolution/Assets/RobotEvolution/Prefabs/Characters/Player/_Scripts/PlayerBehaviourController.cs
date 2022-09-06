using UnityEngine;

[RequireComponent(typeof(InputJoystick))]
public class PlayerBehaviourController : AbsCharacterBehaviourController
{
    private InputJoystick _inputJoystick;

    public override void Awake()
    {
        TryGetComponent(out InputJoystick inputJoystick); _inputJoystick = inputJoystick;
        base.Awake();
    }

    public override void InitSpeñialBehaviours()
    {
        _charactersBehaviourDicionary[typeof(PlayerBehaviourRun)] = new PlayerBehaviourRun(this, _inputJoystick);
    }

    public override void SetBehaviourRun()
    {
        AbsCharacterBaseBehaviour playerBehaviourRun = GetBehaviourFromDictionary<PlayerBehaviourRun>();
        SetNewCharacterBehaviour(playerBehaviourRun);
    }
}
